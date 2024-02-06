package com.encyclopediagalactica.document;

import java.util.ArrayList;
import java.util.List;

import org.springframework.stereotype.Service;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.document.model.DocumentEntity;

@Service
public class DocumentTestUtils {

    public Iterable<DocumentEntity> createDocumentEntities(int amount) {
        List<DocumentEntity> result = new ArrayList<>();

        for (int i = 0; i < amount; i++) {
            result.add(DocumentEntity.builder().id((long) i).name("name" + i).desc("desc" + i)
                    .build());
        }
        return result.stream().toList();
    }

    /**
     * Creates the provided amount of unique {@link Document} entities.
     *
     * @param amount the amount of entities will be created
     * @return {@link List<Document>} result
     */
    public Iterable<Document> createDocuments(int amount) {
        List<Document> result = new ArrayList<>();

        for (int i = 0; i < amount; i++) {
            result.add(Document.builder().setId("100" + i).setName("name " + i).setDesc("desc " + i)
                    .build());
        }
        return result.stream().toList();
    }
}
