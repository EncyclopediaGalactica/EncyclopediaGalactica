package com.andrascsanyi.encyclopediagalactica.iam.validation.rules;

import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleInput;
import java.util.stream.Stream;
import org.junit.jupiter.api.extension.ExtensionContext;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.ArgumentsProvider;

public class ModuleInput_InvalidDataSetFor_AddModuleScenario implements ArgumentsProvider {

    @Override
    public Stream<? extends Arguments> provideArguments(ExtensionContext extensionContext) {
        return Stream.of(
            Arguments.of(ModuleInput.builder().id(100L).name("asd").description("asd").build()),
            Arguments.of(ModuleInput.builder().id(0L).name(null).description("asd").build()),
            Arguments.of(ModuleInput.builder().id(0L).name("").description("asd").build()),
            Arguments.of(ModuleInput.builder().id(0L).name("   ").description("asd").build()),
            Arguments.of(ModuleInput.builder().id(0L)
                .name(
                    "qwertyuioplkjhgfdsazxcqwertyuiopasdfghjklmnbvcxzqwertyuioplkjhgfdsazxcvbnmmnbvcx"
                        + "zasdfghjklpoiuytrewqqwertyuiopasdfghjklmnbvcxzqwertyuioplkjhgfdsazxcvbnmmn"
                        + "bvcxzasdfghjklpoiuytrewqqwertyuiopasdfghjklmnbvcxzqwertyuioplkjhgfdsazxcvb"
                        + "nmmnbvcxzasdfghjklpoiuytrewq"
                )
                .description("asd").build()),
            Arguments.of(ModuleInput.builder().id(0L).name("asd").description(null).build()),
            Arguments.of(ModuleInput.builder().id(0L).name("asd").description("").build()),
            Arguments.of(ModuleInput.builder().id(0L).name("asd").description("   ").build()),
            Arguments.of(ModuleInput.builder().id(0L).name("asd")
                .description(
                    "qwertyuioplkjhgfdsazxcqwertyuiopasdfghjklmnbvcxzqwertyuioplkjhgfdsazxcvbnmmnbvcx"
                        + "zasdfghjklpoiuytrewqqwertyuiopasdfghjklmnbvcxzqwertyuioplkjhgfdsazxcvbnmmn"
                        + "bvcxzasdfghjklpoiuytrewqqwertyuiopasdfghjklmnbvcxzqwertyuioplkjhgfdsazxcvb"
                        + "nmmnbvcxzasdfghjklpoiuytrewq"
                ).build())
        );
    }
}
