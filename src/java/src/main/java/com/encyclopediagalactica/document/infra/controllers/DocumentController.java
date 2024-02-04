package com.encyclopediagalactica.document.infra.controllers;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.document.scenarios.GetDocumentByIdScenario;
import com.encyclopediagalactica.document.scenarios.GetDocumentsScenario;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.QueryMapping;
import org.springframework.stereotype.Controller;

import java.util.List;

@Controller
public class DocumentController {

    private final GetDocumentsScenario getDocumentsScenario;
    private final GetDocumentByIdScenario getDocumentByIdScenario;

    public DocumentController(
            GetDocumentsScenario getDocumentsScenario,
            GetDocumentByIdScenario getDocumentByIdScenario) {
        this.getDocumentsScenario = getDocumentsScenario;
        this.getDocumentByIdScenario = getDocumentByIdScenario;
    }

    @QueryMapping(name = "getDocuments")
    public List<Document> getDocuments() {
        return getDocumentsScenario.getAll();
    }

    @QueryMapping(name = "getDocument")
    public Document getDocument(@Argument(name = "id") Long id) {
        return getDocumentByIdScenario.getById(id);
    }
//
//    @MutationMapping(name = "createDocument")
//    public Document createDocument(@Argument("documentInput") Document documentDto) {
//        return documentBusinessLogic.createDocument(documentDto);
//    }
//
//    @MutationMapping(name = "modifyDocument")
//    public Document modifyDocument(
//            @Argument("documentId") Long documentId,
//            @Argument("document") Document documentDto) {
//        return documentBusinessLogic.modifyDocument(documentId, documentDto);
//    }
}
