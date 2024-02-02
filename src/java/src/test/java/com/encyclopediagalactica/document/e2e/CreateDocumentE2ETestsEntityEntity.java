package com.encyclopediagalactica.document.e2e;

import com.encyclopediagalactica.document.dto.DocumentDto;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.graphql.tester.AutoConfigureHttpGraphQlTester;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.graphql.test.tester.HttpGraphQlTester;
import org.springframework.test.annotation.DirtiesContext;

import java.util.HashMap;
import java.util.Map;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
@AutoConfigureHttpGraphQlTester
@DirtiesContext(classMode = DirtiesContext.ClassMode.AFTER_EACH_TEST_METHOD)
public class CreateDocumentE2ETestsEntityEntity {

    @Autowired
    private HttpGraphQlTester httpGraphQlTester;

    @Test
    public void shouldCreateDocument_andReturnIt() {

        // Arrange
        String name = "name";
        String desc = "desc";
        Map<String, Object> input = new HashMap<>();
        input.put("id", 0L);
        input.put("name", name);
        input.put("desc", desc);

        // Act
        DocumentDto result = httpGraphQlTester.document("""
                           mutation mut($input: DocumentInput!) {
                             createDocument(documentInput: $input) {
                               id
                               name
                               desc
                             }
                           }
                        """)
                .variable("input", input)
                .execute()
                .path("createDocument")
                .entity(DocumentDto.class)
                .get();

        // Assert
        assertThat(result.getId()).isGreaterThan(0);
        assertThat(result.getName()).isEqualTo(name);
        assertThat(result.getDesc()).isEqualTo(desc);
    }

}
