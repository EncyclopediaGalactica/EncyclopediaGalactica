package com.andrascsanyi.encyclopediagalactica.iam.entities;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import org.hibernate.validator.constraints.Length;

@Entity
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
@Table(name = "module")
public class Module {

    @Id
    @Column(name = "id")
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(
        name = "name",
        unique = true)
    @Length(
        min = 3,
        max = 255,
        message = "Name has to be between 3 and 255!"
    )
    private String name;

    @Column(name = "description")
    @Length(
        min = 3,
        max = 255,
        message = "Name has to be between 3 and 255!"
    )
    private String description;


}
