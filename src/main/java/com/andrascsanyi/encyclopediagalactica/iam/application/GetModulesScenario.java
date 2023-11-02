package com.andrascsanyi.encyclopediagalactica.iam.application;

import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleOutput;
import java.util.List;

/**
 * Get Modules interface. It provides method to get the {@link Module} objects represented by
 * {@link ModuleOutput}
 */
public interface GetModulesScenario {

    /**
     * Returns the list of {@link ModuleOutput} representing {@link Module} objects in the system.
     *
     * @return list of {@link ModuleOutput}
     */
    List<ModuleOutput> getModules();
}
