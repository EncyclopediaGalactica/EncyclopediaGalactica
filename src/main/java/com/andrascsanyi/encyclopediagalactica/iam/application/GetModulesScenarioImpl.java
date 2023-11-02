package com.andrascsanyi.encyclopediagalactica.iam.application;

import com.andrascsanyi.encyclopediagalactica.iam.application.exceptions.UnknownIAMModuleException;
import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleOutput;
import com.andrascsanyi.encyclopediagalactica.iam.entities.Module;
import com.andrascsanyi.encyclopediagalactica.iam.infra.mappers.ModuleMappers;
import com.andrascsanyi.encyclopediagalactica.iam.infra.repository.ModuleRepository;
import java.util.ArrayList;
import java.util.List;
import lombok.NonNull;
import org.springframework.stereotype.Service;

@Service
public class GetModulesScenarioImpl implements GetModulesScenario {

    private final ModuleRepository moduleRepository;
    private final ModuleMappers moduleMappers;

    public GetModulesScenarioImpl(
        @NonNull ModuleRepository moduleRepository,
        @NonNull ModuleMappers moduleMappers) {
        this.moduleRepository = moduleRepository;
        this.moduleMappers = moduleMappers;
    }

    @Override
    public List<ModuleOutput> getModules() {
        try {
            return getModulesLogic();
        } catch (Exception e) {
            throw new UnknownIAMModuleException("Unknown error happened.", e);
        }
    }

    private List<ModuleOutput> getModulesLogic() {
        List<Module> modules = new ArrayList<>();
        moduleRepository.findAll().forEach(modules::add);
        return moduleMappers.mapModuleListToModuleOutputList(modules);
    }
}
