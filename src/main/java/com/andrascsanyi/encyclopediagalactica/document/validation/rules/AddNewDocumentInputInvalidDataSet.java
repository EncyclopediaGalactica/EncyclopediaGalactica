package com.andrascsanyi.encyclopediagalactica.document.validation.rules;

import com.andrascsanyi.encyclopediagalactica.document.contracts.DocumentInput;
import java.util.stream.Stream;
import org.junit.jupiter.api.extension.ExtensionContext;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.ArgumentsProvider;

public class AddNewDocumentInputInvalidDataSet implements ArgumentsProvider {

    @Override
    public Stream<? extends Arguments> provideArguments(ExtensionContext extensionContext) {
        return Stream.of(
            Arguments.of(DocumentInput.builder().id(100L).name("asd").description("asd").build()),
            Arguments.of(DocumentInput.builder().id(0L).name(null).description("asd").build()),
            Arguments.of(DocumentInput.builder().id(0L).name("").description("asd").build()),
            Arguments.of(DocumentInput.builder().id(0L).name("   ").description("asd").build()),
            Arguments.of(DocumentInput.builder().id(0L)
                .name(
                    "qwertyuioplkjhgfdsazxcqwertyuiopasdfghjklmnbvcxzqwertyuioplkjhgfdsazxcvbnmmnbvcx"
                        + "zasdfghjklpoiuytrewqqwertyuiopasdfghjklmnbvcxzqwertyuioplkjhgfdsazxcvbnmmn"
                        + "bvcxzasdfghjklpoiuytrewqqwertyuiopasdfghjklmnbvcxzqwertyuioplkjhgfdsazxcvb"
                        + "nmmnbvcxzasdfghjklpoiuytrewq"
                )
                .description("asd").build()),
            Arguments.of(DocumentInput.builder().id(0L).name("asd").description(null).build()),
            Arguments.of(DocumentInput.builder().id(0L).name("asd").description("").build()),
            Arguments.of(DocumentInput.builder().id(0L).name("asd").description("   ").build()),
            Arguments.of(DocumentInput.builder().id(0L).name("asd")
                .description(
                    "qwertyuioplkjhgfdsazxcqwertyuiopasdfghjklmnbvcxzqwertyuioplkjhgfdsazxcvbnmmnbvcx"
                        + "zasdfghjklpoiuytrewqqwertyuiopasdfghjklmnbvcxzqwertyuioplkjhgfdsazxcvbnmmn"
                        + "bvcxzasdfghjklpoiuytrewqqwertyuiopasdfghjklmnbvcxzqwertyuioplkjhgfdsazxcvb"
                        + "nmmnbvcxzasdfghjklpoiuytrewq"
                ).build())
        );
    }
}
