using AutoMapper;
using Newtonsoft.Json;
using PP.CompanyManagement.Core;
using PP.CompanyManagement.Core.Business.Managers;
using PP.CompanyManagement.Core.Contracts.Dto;
using PP.CompanyManagement.Core.Entities;
using PP.CompanyManagement.Core.Exceptions.Dto;
using PP.CompanyManagement.Core.Exceptions.Employee;
using PP.CompanyManagement.Core.Interfaces.Persistence.Common;
using PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagementMainDbStorage.Context;
using PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagementMainDbStorage.Repositories;
using PP.CompanyManagement.Core.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace PP.CompanyManagement.Business.Managers
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IUnitOfWork<ICompanyManagementMainDbContext> unitOfWork;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        public EmployeeManager(IUnitOfWork<ICompanyManagementMainDbContext> unitOfWork,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            return this.mapper.Map<IEnumerable<EmployeeDto>>(await this.employeeRepository.SelectAllAsync());
        }

        public async Task<EmployeeDto> Create(CreateUpdateEmployeeDto employeeDto)
        {
            this.ValidateForCreateOrUpdate(employeeDto);

            // TODO: check if already exists?

            var entity = this.mapper.Map<EmployeeEntity>(employeeDto);

            this.employeeRepository.Insert(entity);

            await this.unitOfWork.SaveAsync();

            return this.mapper.Map<EmployeeDto>(entity);
        }

        public async Task<IEnumerable<ImportEmployeeResult>> Import(Stream jsonStream,
            int batchSize = 1000,
            IProgress<int> progress = null,
            CancellationToken cancellationToken = default)
        {
            using StreamReader sr = new StreamReader(jsonStream);
            using JsonTextReader reader = new JsonTextReader(sr);
            reader.SupportMultipleContent = true;
            var serializer = new JsonSerializer();

            int bufferDefaultSize = batchSize > 10000 ? 10000 : batchSize;
            List<ImportEmployeeDto> buffer = new List<ImportEmployeeDto>(bufferDefaultSize);
            Task<IEnumerable<ImportEmployeeResult>> importRepoTask = null;
            List<ImportEmployeeResult> result = new List<ImportEmployeeResult>(bufferDefaultSize);

            await this.unitOfWork.BeginTransactionAsync();

            try
            {
                while (reader.Read())
                {
#if DEBUG
                    //TODO: remove. for test only.
                    //await Task.Delay(0);
#endif

                    cancellationToken.ThrowIfCancellationRequested();

                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        ImportEmployeeDto employeeDto = serializer.Deserialize<ImportEmployeeDto>(reader);
                        this.ValidateForImport(employeeDto);

                        buffer.Add(employeeDto);

                        if (buffer.Count >= batchSize)
                        {
                            if (importRepoTask != null)
                            {
                                // when we have enough items in buffer we wait previous task
                                result.AddRange(await importRepoTask);
                            }
                            // and starts new task
                            importRepoTask = this.employeeRepository.Import(buffer);
                            buffer = new List<ImportEmployeeDto>(bufferDefaultSize);
                        }
                    }

                    //TODO: progress ?
                    //if (stream.CanSeek)
                    //{
                    //    sr.BaseStream.
                    //}
                    //progress?.Report(reader.po);
                }

                if (importRepoTask != null)
                {
                    result.AddRange(await importRepoTask);
                }
                // if itemsCount % batchSize != 0, we will get some unprocessed items in buffer
                if (buffer.Any())
                {
                    result.AddRange(await  this.employeeRepository.Import(buffer));
                }

                await this.unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch (JsonException jsonException)
            {
                await this.unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw new ImportEmployeeValidationException(new string[] { "Invalid file or Json." }, innerException: jsonException);
            }
            catch
            {
                await this.unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }

            return result;
        }

        public async Task<EmployeeDto> Update(Guid id, CreateUpdateEmployeeDto employeeDto)
        {
            this.ValidateForCreateOrUpdate(employeeDto);

            // TODO: check by business ID if exists with another ID?

            var existing = await this.employeeRepository.FindAsync(id) ?? throw new EmployeeNotFoundException("Employee");

            this.mapper.Map(employeeDto, existing);

            this.employeeRepository.Update(existing);

            await this.unitOfWork.SaveAsync();

            return this.mapper.Map<EmployeeDto>(existing);
        }

        private void ValidateForCreateOrUpdate(CreateUpdateEmployeeDto employeeDto)
        {
            List<string> errors = new List<string>();
            this.TryValidateBusinessId(ref errors, employeeDto.BusinessId);

            if (errors.Any())
            {
                throw new CreateEmployeeValidationException(errors, "Invalid Employee.");
            }
        }

        private void ValidateForImport(ImportEmployeeDto employeeDto)
        {
            List<string> errors = new List<string>();
            this.TryValidateBusinessId(ref errors, employeeDto.BusinessId);

            if (errors.Any())
            {
                throw new ImportEmployeeValidationException(errors, "Invalid Employee.");
            }
        }

        private void ValidateBusinessId(EmployeeBusinessIdDto employeeBusinessIdDto)
        {
            List<string> errors = new List<string>();
            this.TryValidateBusinessId(ref errors, employeeBusinessIdDto);

            if (errors.Any())
            {
                throw new CreateEmployeeValidationException(errors, "Invalid Employee BusinessId.");
            }
        }

        private void TryValidateBusinessId(ref List<string> errors, EmployeeBusinessIdDto employeeBusinessIdDto)
        {
            if (employeeBusinessIdDto == null)
            {
                errors.Add("BusinessId object required.");
            }
            else if (!EmployeeBusinessIdValidatorHelper.TryValidateEmployeeBusinessId(employeeBusinessIdDto.PersonnelNumber, employeeBusinessIdDto.Type, out IEnumerable<string> bIdErrors))
            {
                errors.AddRange(bIdErrors);
            }
        }
    }
}
