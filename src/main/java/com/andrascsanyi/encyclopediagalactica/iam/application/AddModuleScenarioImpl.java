package com.andrascsanyi.encyclopediagalactica.iam.application;

import com.andrascsanyi.encyclopediagalactica.common.guard.Guards;
import com.andrascsanyi.encyclopediagalactica.common.guard.exceptions.ObjectIsNullException;
import com.andrascsanyi.encyclopediagalactica.iam.application.exceptions.InputValidationException;
import com.andrascsanyi.encyclopediagalactica.iam.application.exceptions.UnknownIAMModuleException;
import com.andrascsanyi.encyclopediagalactica.iam.application.exceptions.ValueAlreadyExistsException;
import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleInput;
import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleOutput;
import com.andrascsanyi.encyclopediagalactica.iam.entities.Module;
import com.andrascsanyi.encyclopediagalactica.iam.infra.mappers.ModuleMappers;
import com.andrascsanyi.encyclopediagalactica.iam.infra.repository.ModuleRepository;
import com.andrascsanyi.encyclopediagalactica.iam.validation.AddModuleScenarioValidator;
import lombok.NonNull;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.dao.OptimisticLockingFailureException;
import org.springframework.stereotype.Service;

@Service
public class AddModuleScenarioImpl implements AddModuleScenario {

    private final AddModuleScenarioValidator validator;
    private final Guards guards;
    private final ModuleMappers moduleMappers;
    private final ModuleRepository moduleRepository;
    private final static Logger logger = LoggerFactory.getLogger(AddModuleScenarioImpl.class);

    public AddModuleScenarioImpl(
        @NonNull AddModuleScenarioValidator validator,
        @NonNull Guards guards,
        @NonNull ModuleMappers moduleMappers,
        @NonNull ModuleRepository moduleRepository) {
        this.validator = validator;
        this.guards = guards;
        this.moduleMappers = moduleMappers;
        this.moduleRepository = moduleRepository;
    }

    @Override
    public ModuleOutput addModule(ModuleInput dto) {
        try {
            return addModuleBusinessLogic(dto);
        } catch (ObjectIsNullException |
                 com.andrascsanyi.encyclopediagalactica.common.validator.exceptions.ValidationException |
                 IllegalArgumentException e) {
            String message = "Input validation error";
            logger.error(message, e);
            throw new InputValidationException(message, e);
        } catch (OptimisticLockingFailureException e) {
            String message = "Unknown error";
            logger.error(message, e);
            throw new UnknownIAMModuleException(message, e);
        } catch (DataIntegrityViolationException e) {
            String message = "Name value must be unique";
            logger.error(message, e);
            throw new ValueAlreadyExistsException(message, e);
        }
    }

    private ModuleOutput addModuleBusinessLogic(ModuleInput input) {
        guards.ObjectGuards().throwIfNull(input);
        validator.validateAndThrow(input);
        Module module = moduleMappers.mapModuleInputToModule(input);
        Module recordedModule = moduleRepository.save(module);
        return moduleMappers.mapModuleToModuleOutput(recordedModule);
    }
}
