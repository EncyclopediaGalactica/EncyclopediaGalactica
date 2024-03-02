package com.encyclopediagalactica.document;

import com.encyclopediagalactica.api.graphql.DocumentInput;
import com.encyclopediagalactica.api.graphql.DocumentResult;
import com.encyclopediagalactica.ctx.ScenarioContext;
import com.encyclopediagalactica.document.model.DocumentEntity;
import com.encyclopediagalactica.document.scenarios.CreateDocumentScenario;
import com.encyclopediagalactica.document.scenarios.DeleteDocumentScenario;
import com.encyclopediagalactica.document.scenarios.GetDocumentsScenario;
import com.encyclopediagalactica.document.scenarios.ModifyDocumentScenario;
import com.encyclopediagalactica.utils.DocumentTestUtilsImp;
import com.encyclopediagalactica.utils.InputStringUtils;
import io.cucumber.java.Before;
import io.cucumber.java.en.And;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.springframework.beans.factory.annotation.Autowired;

import java.util.List;

import static org.assertj.core.api.Assertions.assertThat;


public class DocumentScenarioStepDefinitions {
    
    private static final String OPERATION_EXCEPTION = "operationException";
    
    private final static String OPERATION_RESULT_DOCUMENT_LIST = "operationResultDocumentList";
    
    private static final String OPERATION_RESULT_DOCUMENT = "operationResultDocument";
    
    private final static String DOCUMENT_INPUT_BUILDER = "documentInputBuilder";
    
    @Autowired
    private GetDocumentsScenario getDocumentsScenario;
    
    @Autowired
    private CreateDocumentScenario createDocumentScenario;
    
    @Autowired
    private ModifyDocumentScenario modifyDocumentScenario;
    
    @Autowired
    private DeleteDocumentScenario deleteDocumentScenario;
    
    @Autowired
    private DocumentTestUtilsImp documentTestUtilsImp;
    
    @Autowired
    private ScenarioContext scenarioContext;
    
    @Autowired
    private InputStringUtils inputStringUtils;
    
    @Before
    public void init() {
        
        scenarioContext.clear();
    }
    
    @Given("we have {int} documents in the system already")
    public void weHaveGivenDocumentsInTheSystemAlready(int amount) {
        
        Iterable<DocumentInput> testDocuments = documentTestUtilsImp.createDocuments(amount, true);
        for (DocumentInput d : testDocuments) {
            createDocumentScenario.create(d);
        }
    }
    
    @When("the list of documents are requested")
    public void whenTheListOfDocumentsRequested() {
        
        List<DocumentResult> documentsList = getDocumentsScenario.getAll();
        scenarioContext.add(OPERATION_RESULT_DOCUMENT_LIST, documentsList);
    }
    
    @Then("the result list length is {int}")
    public void theResultListLengthIsExpected(int expectedLength) {
        
        List<DocumentEntity> result = (List<DocumentEntity>) scenarioContext.get(OPERATION_RESULT_DOCUMENT_LIST);
        assertThat(result.size()).isEqualTo(expectedLength);
    }
    
    @Given("there is a Document entity")
    public void thereIsADocumentEntity() {
        
        scenarioContext.add(DOCUMENT_INPUT_BUILDER, new DocumentInput.Builder());
    }
    
    @And("the id is {int}")
    public void theIdIsId(Integer id) {
        
        DocumentInput.Builder builder = (DocumentInput.Builder) scenarioContext.get(DOCUMENT_INPUT_BUILDER);
        builder.setId(id.toString());
        scenarioContext.add(DOCUMENT_INPUT_BUILDER, builder);
    }
    
    @And("the name is {string}")
    public void theNameIsName(String name) {
        
        DocumentInput.Builder builder = (DocumentInput.Builder) scenarioContext.get(DOCUMENT_INPUT_BUILDER);
        String modifiedValue = inputStringUtils.provideValue(name);
        builder.setName(modifiedValue);
        scenarioContext.add(DOCUMENT_INPUT_BUILDER, builder);
    }
    
    @And("the description is {string}")
    public void theDescriptionIsDescription(String description) {
        
        DocumentInput.Builder builder = (DocumentInput.Builder) scenarioContext.get(DOCUMENT_INPUT_BUILDER);
        String modifiedValue = inputStringUtils.provideValue(description);
        builder.setDesc(modifiedValue);
        scenarioContext.add(DOCUMENT_INPUT_BUILDER, builder);
    }
    
    @When("the Document is created")
    public void theDocumentIsCreated() {
        
        DocumentInput.Builder builder = (DocumentInput.Builder) scenarioContext.get(DOCUMENT_INPUT_BUILDER);
        DocumentInput result = builder.build();
        DocumentResult createdDocument = null;
        try {
            createdDocument = createDocumentScenario.create(result);
            scenarioContext.add(OPERATION_RESULT_DOCUMENT, createdDocument);
        } catch (Exception e) {
            scenarioContext.add(OPERATION_EXCEPTION, e);
            scenarioContext.remove(OPERATION_RESULT_DOCUMENT);
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
        
        assertThat(scenarioContext.containsKey(OPERATION_RESULT_DOCUMENT)).isTrue();
    }
    
    @And("the name value is {string}")
    public void theNameValueIsName(String word) {
        
        DocumentResult result = (DocumentResult) scenarioContext.get(OPERATION_RESULT_DOCUMENT);
        assertThat(result.getName()).isEqualTo(word);
    }
    
    @And("the description value is {string}")
    public void theDescriptionValueIsDescription(String str) {
        
        DocumentResult result = (DocumentResult) scenarioContext.get(OPERATION_RESULT_DOCUMENT);
        assertThat(result.getDesc()).isEqualTo(str);
    }
    
    @And("the id value is greater than {int}")
    public void theIdValueIsGreaterThan(Integer i) {
        
        DocumentResult result = (DocumentResult) scenarioContext.get(OPERATION_RESULT_DOCUMENT);
        assertThat(Long.parseLong(result.getId())).isGreaterThan(i);
    }
    
    @And("the name is changed to {string}")
    public void theNameIsChangeToName(String str) {
        
        DocumentInput documentInput = (DocumentInput) scenarioContext.get(OPERATION_RESULT_DOCUMENT);
        String input = inputStringUtils.provideValue(str);
        documentInput.setName(input);
        scenarioContext.add(OPERATION_RESULT_DOCUMENT, documentInput);
    }
    
    @And("the description is changed to {string}")
    public void theDescriptionIsChangeToDescription(String str) {
        
        DocumentInput document = (DocumentInput) scenarioContext.get(OPERATION_RESULT_DOCUMENT);
        String input = inputStringUtils.provideValue(str);
        document.setDesc(input);
        scenarioContext.add(OPERATION_RESULT_DOCUMENT, document);
    }
    
    @When("the Document is modified")
    public void theDocumentIsSaved() {
        
        DocumentInput document = (DocumentInput) scenarioContext.get(OPERATION_RESULT_DOCUMENT);
        DocumentResult result = null;
        try {
            result = modifyDocumentScenario.modify(document);
            scenarioContext.add(OPERATION_RESULT_DOCUMENT, result);
        } catch (Exception e) {
            scenarioContext.add(OPERATION_EXCEPTION, e);
            scenarioContext.remove(OPERATION_RESULT_DOCUMENT);
        }
    }
    
    @When("the Document with id {int} is deleted")
    public void theDocumentWithIdIsDeleted(int id) {
        
        try {
            deleteDocumentScenario.delete(Long.valueOf(id));
        } catch (Exception e) {
            scenarioContext.add(OPERATION_EXCEPTION, e);
        }
    }
    
    @Then("{string} message is returned")
    public void deletingDocumentProcessFailedMessageIsReturned(String msg) {
        
        Exception exception = (Exception) scenarioContext.get(OPERATION_EXCEPTION);
        assertThat(exception.getMessage()).isEqualTo(msg);
    }
    
    @Then("the api returns no error")
    public void theApiReturnsNoError() {
        
        assertThat(scenarioContext.containsKey(OPERATION_EXCEPTION)).isFalse();
    }
    
    @When("the Document is deleted")
    public void theDocumentIsDeleted() {
        
        DocumentResult document = (DocumentResult) scenarioContext.get(OPERATION_RESULT_DOCUMENT);
        try {
            deleteDocumentScenario.delete(Long.parseLong(document.getId()));
        } catch (Exception e) {
            scenarioContext.add(OPERATION_EXCEPTION, e);
        }
    }
    
    @And("the newly created Document modification starts")
    public void theNewlyCreatedDocumentModificationStarts() {
        
        DocumentResult result = (DocumentResult) scenarioContext.get(OPERATION_RESULT_DOCUMENT);
        DocumentInput input = DocumentInput.builder()
            .setId(result.getId())
            .setName(result.getName())
            .setDesc(result.getDesc())
            .build();
        scenarioContext.remove(OPERATION_RESULT_DOCUMENT);
        scenarioContext.add(OPERATION_RESULT_DOCUMENT, input);
    }
}
