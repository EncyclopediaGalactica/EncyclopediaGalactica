package com.andrascsanyi.encyclopediagalactica.iam.infra.mappers;

import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleInput;
import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleOutput;
import com.andrascsanyi.encyclopediagalactica.iam.entities.Module;
import java.util.List;
import org.mapstruct.InjectionStrategy;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;
import org.mapstruct.factory.Mappers;

@Mapper(componentModel = "spring", injectionStrategy = InjectionStrategy.CONSTRUCTOR)
public interface ModuleMappers {

    ModuleMappers INSTANCE = Mappers.getMapper(ModuleMappers.class);

    List<ModuleOutput> mapModuleListToModuleOutputList(List<Module> moduleList);

    @Mapping(source = "id", target = "id")
    @Mapping(source = "name", target = "name")
    @Mapping(source = "description", target = "description")
    Module mapModuleInputToModule(ModuleInput input);

    @Mapping(source = "id", target = "id")
    @Mapping(source = "name", target = "name")
    @Mapping(source = "description", target = "description")
    ModuleOutput mapModuleToModuleOutput(Module module);
}
