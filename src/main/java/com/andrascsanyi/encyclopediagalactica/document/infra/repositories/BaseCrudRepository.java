package com.andrascsanyi.encyclopediagalactica.document.infra.repositories;

import org.springframework.data.repository.ListCrudRepository;
import org.springframework.data.repository.NoRepositoryBean;

@NoRepositoryBean
public interface BaseCrudRepository<T, Long> extends ListCrudRepository<T, Long> {

    <S extends T> S save(S entity);

}
