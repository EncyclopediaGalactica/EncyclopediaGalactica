package com.encyclopediagalactica.document.mappers;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.document.entities.DocumentEntity;
import org.mapstruct.Mapper;
import org.mapstruct.Mappings;
import org.mapstruct.factory.Mappers;

@Mapper
public interface DocumentEntityMapper {

    DocumentEntityMapper INSTANCE = Mappers.getMapper(DocumentEntityMapper.class);

    @Mappings({})
    DocumentEntity mapDocumentToDocumentEntity(Document document);

    @Mappings({})
    Document mapDocumentEntityToDocument(DocumentEntity savedEntity);
}
