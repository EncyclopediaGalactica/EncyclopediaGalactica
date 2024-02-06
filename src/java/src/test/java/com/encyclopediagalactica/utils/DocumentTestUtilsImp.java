package com.encyclopediagalactica.utils;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.document.model.DocumentEntity;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class DocumentTestUtilsImp implements DocumentTestUtils {

    @Override
    public Iterable<DocumentEntity> createDocumentEntities(int amount, Boolean isIdZero) {
        List<DocumentEntity> result = new ArrayList<>();

        for (int i = 0; i < amount; i++) {
            result.add(DocumentEntity.builder().id((long) i).name("name" + i).desc("desc" + i)
                    .build());
        }
        return result.stream().toList();
    }

    @Override
    public Iterable<Document> createDocuments(int amount, Boolean isIdZero) {
        List<Document> result = new ArrayList<>();

        for (int i = 0; i < amount; i++) {
            Document.Builder builder = Document.builder();
            if (!isIdZero) {
                builder.setId("100" + i);
            }
            builder.setName("name " + i)
                    .setDesc("desc " + i);
            result.add(builder.build());
        }
        return result.stream().toList();
    }
}
