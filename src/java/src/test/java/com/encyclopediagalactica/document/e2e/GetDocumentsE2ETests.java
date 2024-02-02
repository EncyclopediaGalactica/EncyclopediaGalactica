package com.encyclopediagalactica.document.e2e;

import com.encyclopediagalactica.document.dto.DocumentDto;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.graphql.tester.AutoConfigureHttpGraphQlTester;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.graphql.test.tester.HttpGraphQlTester;
import org.springframework.test.annotation.DirtiesContext;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.stream.Stream;

import static org.assertj.core.api.Assertions.assertThat;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
@AutoConfigureHttpGraphQlTester
@DirtiesContext(classMode = DirtiesContext.ClassMode.AFTER_EACH_TEST_METHOD)
public class GetDocumentsE2ETests {

    @Autowired
    private HttpGraphQlTester graphQlTester;

    private static Stream<Arguments> returnsAllDocuments_data() {
        return Stream.of(
            Arguments.of(0, 0),
            Arguments.of(1, 1),
            Arguments.of(2, 2)
        );
    }

    @ParameterizedTest
    @MethodSource("returnsAllDocuments_data")
    public void returnsAllDocuments(Integer amountOfData, Integer expectedAmount) {

        // Arrange
        prepareTestData(amountOfData, graphQlTester);

        // Act
        List<DocumentDto> result = graphQlTester.document("""
                query getDocuments {
                    getDocuments {
                        id
                    }
                }
                """)
            .execute()
            .path("getDocuments")
            .entityList(DocumentDto.class)
            .get();

        // Assert
        assertThat(result.size()).isEqualTo(expectedAmount);

    }

    private void prepareTestData(Integer amount, HttpGraphQlTester graphQlTester) {
        if (amount < 1) {
            return;
        }

        for (int i = 0; i < amount; i++) {
            Map<String, Object> input = new HashMap<>();
            input.put("id", 0L);
            input.put("name", "name" + 1);

            graphQlTester.document("""
                       mutation mut($input: DocumentInput!) {
                         createDocument(documentInput: $input) {
                           id
                         }
                       }
                    """)
                .variable("input", input)
                .execute();
        }
    }
}
