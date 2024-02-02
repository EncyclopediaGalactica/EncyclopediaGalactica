package com.encyclopediagalactica.document.controllers;

import com.encyclopediagalactica.document.dto.DocumentDto;
import com.encyclopediagalactica.document.scenarios.DocumentBusinessLogic;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.MutationMapping;
import org.springframework.graphql.data.method.annotation.QueryMapping;
import org.springframework.stereotype.Controller;

import java.util.List;

@Controller
public class DocumentController {

    private final Logger logger = LoggerFactory.getLogger(DocumentController.class);
    private final DocumentBusinessLogic documentBusinessLogic;

    public DocumentController(DocumentBusinessLogic documentBusinessLogic) {
        this.documentBusinessLogic = documentBusinessLogic;
    }

    @QueryMapping(name = "getDocuments")
    public List<DocumentDto> getDocuments() {
        return documentBusinessLogic.getDocuments();
    }

    @QueryMapping(name = "getDocument")
    public DocumentDto getDocument(Long id) {
        return documentBusinessLogic.getDocument(id);
    }

    @MutationMapping(name = "createDocument")
    public DocumentDto createDocument(@Argument("documentInput") DocumentDto documentDto) {
        return documentBusinessLogic.createDocument(documentDto);
    }

    @MutationMapping(name = "modifyDocument")
    public DocumentDto modifyDocument(
            @Argument("documentId") Long documentId,
            @Argument("document") DocumentDto documentDto) {
        return documentBusinessLogic.modifyDocument(documentId, documentDto);
    }
}
