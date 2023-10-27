package com.andrascsanyi.encyclopediagalactica.document.infra.mappers;

import static org.assertj.core.api.Assertions.assertThat;

import com.andrascsanyi.encyclopediagalactica.document.contracts.DocumentInput;
import com.andrascsanyi.encyclopediagalactica.document.entities.Document;
import org.junit.jupiter.api.Test;

public class DocumentMappersTests {

    @Test
    void mappersTest() {
        // Arrange
        DocumentInput input = DocumentInput.builder()
            .id(100L)
            .name("name")
            .description("description")
            .build();

        // Act
        Document result = DocumentMapper.INSTANCE.documentInputToDocument(input);

        // Assert
        assertThat(result.getId()).isEqualTo(input.getId());
        assertThat(result.getName()).isEqualTo(input.getName());
        assertThat(result.getDescription()).isEqualTo(input.getDescription());
    }

}
