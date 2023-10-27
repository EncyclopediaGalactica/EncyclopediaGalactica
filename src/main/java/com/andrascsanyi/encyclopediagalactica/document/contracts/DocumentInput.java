package com.andrascsanyi.encyclopediagalactica.document.contracts;

import com.andrascsanyi.encyclopediagalactica.common.validator.constraints.TrimmedLength;
import com.andrascsanyi.encyclopediagalactica.document.validation.rules.AddNewDocumentInputValidationRule;
import com.andrascsanyi.encyclopediagalactica.document.validation.rules.DeleteDocumentInput;
import com.andrascsanyi.encyclopediagalactica.document.validation.rules.ModifyDocumentInput;
import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class DocumentInput {

    @Max(
        value = 0,
        message = "Id must be zero",
        groups = AddNewDocumentInputValidationRule.class
    )
    @Min(
        value = 1,
        message = "Id must be greater or equal to 1",
        groups = {
            DeleteDocumentInput.class,
            ModifyDocumentInput.class
        }
    )
    private Long id;

    @NotNull(
        message = "Name must not be null.",
        groups = {
            AddNewDocumentInputValidationRule.class,
            ModifyDocumentInput.class
        }
    )
    @NotBlank(
        message = "Name must not be blank.",
        groups = {
            AddNewDocumentInputValidationRule.class,
            ModifyDocumentInput.class
        }
    )
    @NotEmpty(
        message = "Name must not be empty",
        groups = {
            AddNewDocumentInputValidationRule.class,
            ModifyDocumentInput.class
        }
    )
    @TrimmedLength(
        min = 3,
        max = 255,
        message = "Name's trimmed length must be between 3 and 255",
        groups = {
            AddNewDocumentInputValidationRule.class,
            ModifyDocumentInput.class
        }
    )
    private String name;

    @NotNull(
        message = "Description must not be null.",
        groups = {
            AddNewDocumentInputValidationRule.class,
            ModifyDocumentInput.class
        }
    )
    @NotBlank(
        message = "Description must not be blank.",
        groups = {
            AddNewDocumentInputValidationRule.class,
            ModifyDocumentInput.class
        }
    )
    @NotEmpty(
        message = "Description must not be empty",
        groups = {
            AddNewDocumentInputValidationRule.class,
            ModifyDocumentInput.class
        }
    )
    @TrimmedLength(
        min = 3,
        max = 255,
        message = "Description's trimmed length must be between 3 and 255",
        groups = {
            AddNewDocumentInputValidationRule.class,
            ModifyDocumentInput.class
        }
    )
    private String description;
}
