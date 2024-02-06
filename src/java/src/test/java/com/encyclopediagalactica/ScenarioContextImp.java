package com.encyclopediagalactica;

import org.springframework.stereotype.Service;

import java.util.HashMap;
import java.util.Map;

@Service
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
}
