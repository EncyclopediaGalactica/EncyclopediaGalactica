package com.encyclopediagalactica.document.infra.mappers;

import com.encyclopediagalactica.api.graphql.DocumentInput;
import com.encyclopediagalactica.api.graphql.DocumentResult;
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
    DocumentEntity mapDocumentInputToDocumentEntity(DocumentInput document);
    
    @Mappings({})
    DocumentResult mapDocumentEntityToDocument(DocumentEntity savedEntity);
    
    default List<DocumentResult> mapDocumentEntitiesToDocumentResults
        (List<DocumentEntity> documentEntities) {
        
        return documentEntities.stream()
            .map(this::mapDocumentEntityToDocument)
            .collect(Collectors.toList());
    }
}
