package com.andrascsanyi.encyclopediagalactica.document.infra.mappers;

import com.andrascsanyi.encyclopediagalactica.document.contracts.DocumentInput;
import com.andrascsanyi.encyclopediagalactica.document.contracts.DocumentResult;
import com.andrascsanyi.encyclopediagalactica.document.entities.Document;
import org.mapstruct.InjectionStrategy;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;
import org.mapstruct.factory.Mappers;

@Mapper(componentModel = "spring", injectionStrategy = InjectionStrategy.CONSTRUCTOR)
public interface DocumentMapper {

    DocumentMapper INSTANCE = Mappers.getMapper(DocumentMapper.class);

    @Mapping(source = "id", target = "id")
    @Mapping(source = "name", target = "name")
    @Mapping(source = "description", target = "description")
    Document documentInputToDocument(DocumentInput documentInput);

    @Mapping(source = "id", target = "id")
    @Mapping(source = "name", target = "name")
    @Mapping(source = "description", target = "description")
    DocumentResult documentToDocumentInput(Document document);
}
