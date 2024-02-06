package com.encyclopediagalactica.document;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.ctx.ScenarioContext;
import com.encyclopediagalactica.document.model.DocumentEntity;
import com.encyclopediagalactica.document.scenarios.CreateDocumentScenario;
import com.encyclopediagalactica.document.scenarios.GetDocumentsScenario;
import com.encyclopediagalactica.document.scenarios.ModifyDocumentScenario;
import com.encyclopediagalactica.utils.DocumentTestUtilsImp;
import com.encyclopediagalactica.utils.InputStringUtils;
import io.cucumber.java.en.And;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.springframework.beans.factory.annotation.Autowired;

import java.util.List;

import static org.assertj.core.api.Assertions.assertThat;


public class DocumentStepDefinitions {

    private static final String OPERATION_EXCEPTION = "operationException";
    private static final String DOCUMENT_WORKING_COPY = "documentWorkingCopy";
    @Autowired
    private GetDocumentsScenario getDocumentsScenario;

    @Autowired
    private CreateDocumentScenario createDocumentScenario;

    @Autowired
    private ModifyDocumentScenario modifyDocumentScenario;

    @Autowired
    private DocumentTestUtilsImp documentTestUtilsImp;

    @Autowired
    private ScenarioContext scenarioContext;

    @Autowired
    private InputStringUtils inputStringUtils;

    private final static String DOCUMENT_BUILDER = "documentObject";
    private final static String DOCUMENT_LIST = "documentList";

    @Given("we have {int} documents in the system already")
    public void weHaveGivenDocumentsInTheSystemAlready(int amount) {
        Iterable<Document> testDocuments = documentTestUtilsImp.createDocuments(amount, true);
        for (Document d : testDocuments) {
            createDocumentScenario.create(d);
        }
    }

    @When("the list of documents are requested")
    public void whenTheListOfDocumentsRequested() {
        List<Document> documentsList = getDocumentsScenario.getAll();
        scenarioContext.add(DOCUMENT_LIST, documentsList);
    }

    @Then("the result list length is {int}")
    public void theResultListLengthIsExpected(int expectedLength) {
        List<DocumentEntity> result = (List<DocumentEntity>) scenarioContext.get(DOCUMENT_LIST);
        assertThat(result.size()).isEqualTo(expectedLength);
    }

    @Given("there is a Document entity")
    public void thereIsADocumentEntity() {
        scenarioContext.add(DOCUMENT_BUILDER, new Document.Builder());
    }

    @And("the id is {int}")
    public void theIdIsId(Integer id) {
        Document.Builder builder = (Document.Builder) scenarioContext.get(DOCUMENT_BUILDER);
        builder.setId(id.toString());
        scenarioContext.add(DOCUMENT_BUILDER, builder);
    }

    @And("the name is {string}")
    public void theNameIsName(String name) {
        Document.Builder builder = (Document.Builder) scenarioContext.get(DOCUMENT_BUILDER);
        String modifiedValue = inputStringUtils.provideValue(name);
        builder.setName(modifiedValue);
        scenarioContext.add(DOCUMENT_BUILDER, builder);
    }

    @And("the description is {string}")
    public void theDescriptionIsDescription(String description) {
        Document.Builder builder = (Document.Builder) scenarioContext.get(DOCUMENT_BUILDER);
        String modifiedValue = inputStringUtils.provideValue(description);
        builder.setDesc(modifiedValue);
        scenarioContext.add(DOCUMENT_BUILDER, builder);
    }

    @When("the Document is created")
    public void theDocumentIsCreated() {
        Document.Builder builder = (Document.Builder) scenarioContext.get(DOCUMENT_BUILDER);
        Document result = builder.build();
        Document createdDocument = null;
        try {
            createdDocument = createDocumentScenario.create(result);
            scenarioContext.add(DOCUMENT_WORKING_COPY, createdDocument);
        } catch (Exception e) {
            scenarioContext.add(OPERATION_EXCEPTION, e);
        }
    }

    @Then("{string} message returned")
    public void errorMessageReturned(String errorMessage) {
        Exception exception = (Exception) scenarioContext.get(OPERATION_EXCEPTION);
        String message = exception.getMessage();
        assertThat(message).isEqualTo(errorMessage);
    }

    @Then("the api returns the newly created Document")
    public void theApiReturnsTheNewlyCreatedDocument() {
        if (scenarioContext.get(DOCUMENT_WORKING_COPY) == null) {
            assertThat(false).isTrue();
        }
    }

    @And("the name value is {string}")
    public void theNameValueIsName(String word) {
        Document result = (Document) scenarioContext.get(DOCUMENT_WORKING_COPY);
        assertThat(result.getName()).isEqualTo(word);
    }

    @And("the description value is {string}")
    public void theDescriptionValueIsDescription(String str) {
        Document result = (Document) scenarioContext.get(DOCUMENT_WORKING_COPY);
        assertThat(result.getDesc()).isEqualTo(str);
    }

    @And("the id value is greater than {int}")
    public void theIdValueIsGreaterThan(Integer i) {
        Document result = (Document) scenarioContext.get(DOCUMENT_WORKING_COPY);
        assertThat(Long.parseLong(result.getId())).isGreaterThan(i);
    }

    @And("the name is change to {string}")
    public void theNameIsChangeToName(String str) {
        Document document = (Document) scenarioContext.get(DOCUMENT_WORKING_COPY);
        String input = inputStringUtils.provideValue(str);
        document.setName(input);
        scenarioContext.add(DOCUMENT_WORKING_COPY, document);
    }

    @And("the description is change to {string}")
    public void theDescriptionIsChangeToDescription(String str) {
        Document document = (Document) scenarioContext.get(DOCUMENT_WORKING_COPY);
        String input = inputStringUtils.provideValue(str);
        document.setDesc(input);
        scenarioContext.add(DOCUMENT_WORKING_COPY, document);
    }

    @When("the Document is saved")
    public void theDocumentIsSaved() {
        Document document = (Document) scenarioContext.get(DOCUMENT_WORKING_COPY);
        Document result = null;
        try {
            result = modifyDocumentScenario.modify(document);
            scenarioContext.add(DOCUMENT_WORKING_COPY, result);
        } catch (Exception e) {
            scenarioContext.add(OPERATION_EXCEPTION, e);
        }
    }
}
