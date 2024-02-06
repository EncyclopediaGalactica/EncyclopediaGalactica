package com.encyclopediagalactica.document.infra.mappers;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.document.model.DocumentEntity;
import org.mapstruct.Mapper;
import org.mapstruct.Mappings;
import org.mapstruct.factory.Mappers;

import java.util.List;
import java.util.stream.Collectors;

@Mapper
public interface DocumentEntityMapper {

    DocumentEntityMapper INSTANCE = Mappers.getMapper(DocumentEntityMapper.class);

    @Mappings({})
    DocumentEntity mapDocumentToDocumentEntity(Document document);

    @Mappings({})
    Document mapDocumentEntityToDocument(DocumentEntity savedEntity);

    default List<Document> mapDocumentEntitiesToDocuments(List<DocumentEntity> documentEntities) {

        return documentEntities.stream()
                .map(this::mapDocumentEntityToDocument)
                .collect(Collectors.toList());
    }
}
