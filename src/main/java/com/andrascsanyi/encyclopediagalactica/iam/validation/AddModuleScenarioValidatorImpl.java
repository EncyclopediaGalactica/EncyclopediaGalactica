package com.andrascsanyi.encyclopediagalactica.iam.validation;

import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleInput;
import com.andrascsanyi.encyclopediagalactica.iam.validation.rules.AddModuleScenarioGroup;
import jakarta.validation.ConstraintViolation;
import java.util.Set;
import org.springframework.stereotype.Service;

@Service
public class AddModuleScenarioValidatorImpl
    extends com.encyclopediagalactica.common.validator.ValidatorBase<ModuleInput>
    implements AddModuleScenarioValidator {

    @Override
    public void validateAndThrow(ModuleInput o) {
        Set<ConstraintViolation<ModuleInput>> violations = validator.validate(
            o, AddModuleScenarioGroup.class);
        checkViolationsAndThrow(violations);
    }
}
