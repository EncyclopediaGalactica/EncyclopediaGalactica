package com.encyclopediagalactica.document.infra.controllers;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.api.graphql.DocumentInput;
import com.encyclopediagalactica.document.scenarios.CreateDocumentScenario;
import com.encyclopediagalactica.document.scenarios.DeleteDocumentScenario;
import com.encyclopediagalactica.document.scenarios.GetDocumentByIdScenario;
import com.encyclopediagalactica.document.scenarios.GetDocumentsScenario;
import com.encyclopediagalactica.document.scenarios.ModifyDocumentScenario;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.MutationMapping;
import org.springframework.graphql.data.method.annotation.QueryMapping;
import org.springframework.stereotype.Controller;

import java.util.List;

@Controller
public class DocumentController {

    @Autowired
    private GetDocumentsScenario getDocumentsScenario;

    @Autowired
    private GetDocumentByIdScenario getDocumentByIdScenario;

    @Autowired
    private CreateDocumentScenario createDocumentScenario;

    @Autowired
    private DeleteDocumentScenario deleteDocumentScenario;

    @Autowired
    private ModifyDocumentScenario modifyDocumentScenario;

    public DocumentController() {
    }

    @QueryMapping(name = "getDocuments")
    public List<Document> getDocuments() {
        return getDocumentsScenario.getAll();
    }

    @QueryMapping(name = "getDocument")
    public Document getDocument(@Argument(name = "id") Long id) {
        return getDocumentByIdScenario.getById(id);
    }


    @MutationMapping(name = "createDocument")
    public Document createDocument(
            @Argument("documentInput") DocumentInput documentInput) {
        return createDocumentScenario.create(documentInput);
    }

    @MutationMapping(name = "modifyDocument")
    public Document modifyDocument(
            @Argument("document") DocumentInput documentInput) {
        return modifyDocumentScenario.modify(documentInput);
    }
}
