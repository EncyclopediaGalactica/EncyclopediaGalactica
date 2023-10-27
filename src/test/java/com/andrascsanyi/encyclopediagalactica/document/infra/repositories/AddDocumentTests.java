package com.andrascsanyi.encyclopediagalactica.document.infra.repositories;

import static org.assertj.core.api.Assertions.assertThat;

import com.andrascsanyi.encyclopediagalactica.document.entities.Document;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.AutoConfigureTestEntityManager;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.test.annotation.DirtiesContext;
import org.springframework.test.annotation.DirtiesContext.MethodMode;

@DataJpaTest
@AutoConfigureTestEntityManager
@DirtiesContext(methodMode = MethodMode.AFTER_METHOD)
public class AddDocumentTests {

    @Autowired
    private DocumentRepository documentRepository;

    @Test
    void shouldAdd() {
        // Arrange
        Document document = Document.builder()
            .name("name")
            .description("description")
            .build();

        // Act
        Document result = documentRepository.save(document);

        // Assert
        assertThat(result.getId()).isGreaterThanOrEqualTo(1);
        assertThat(result.getName()).isEqualTo(document.getName());
        assertThat(result.getDescription()).isEqualTo(document.getDescription());
    }
}
