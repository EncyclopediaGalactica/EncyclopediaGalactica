package com.andrascsanyi.encyclopediagalactica.iam.application;

import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleInput;
import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleOutput;

/**
 * Add Module interface It provides functionality to create new {@link Module} object in the
 * system.
 */
public interface AddModuleScenario {

    /**
     * Creates a new {@link  Module} entity in the system based on the provided {@link ModuleOutput}
     * object.
     *
     * @param dto the provided {@link ModuleOutput} object.
     * @return a {@link ModuleOutput} representing the created entity.
     */
    ModuleOutput addModule(ModuleInput dto);

}
