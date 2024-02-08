package com.encyclopediagalactica.ctx;

import io.cucumber.spring.ScenarioScope;
import org.springframework.stereotype.Service;

import java.util.HashMap;
import java.util.Map;

@Service
@ScenarioScope
public class ScenarioContextImp implements ScenarioContext {

    private Map<String, Object> storage = new HashMap<>();

    @Override
    public void add(String key, Object value) {
        storage.put(key, value);
    }

    @Override
    public Object get(String key) {
        if (!storage.containsKey(key)) {
            throw new RuntimeException(String.format("There is no such key: %s", key));
        }
        return storage.get(key);
    }

    @Override
    public Boolean containsKey(String key) {
        return storage.containsKey(key);
    }

    @Override
    public void remove(String key) {
        storage.remove(key);
    }

    @Override
    public void clear() {
        storage.clear();
    }
}
