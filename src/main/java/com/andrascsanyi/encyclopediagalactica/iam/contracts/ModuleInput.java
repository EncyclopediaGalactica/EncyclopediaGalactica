package com.andrascsanyi.encyclopediagalactica.iam.contracts;

import com.andrascsanyi.encyclopediagalactica.common.validator.constraints.TrimmedLength;
import com.andrascsanyi.encyclopediagalactica.iam.validation.rules.AddModuleScenarioGroup;
import jakarta.validation.constraints.Max;
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
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class ModuleInput {

    @Max(
        value = 0,
        message = "Id value must be zero when new entity will be recorded.",
        groups = AddModuleScenarioGroup.class)
    private Long id;

    @NotNull
    @NotBlank
    @NotEmpty
    @TrimmedLength(
        min = 3,
        max = 255,
        message = "Name has to be longer than 3, but less than 255 characters"
    )
    private String name;

    @NotNull
    @NotBlank
    @NotEmpty
    @TrimmedLength(
        min = 3,
        max = 255,
        message = "Name has to be longer than 3, but less than 255 characters"
    )
    private String description;

}
