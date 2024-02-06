package com.encyclopediagalactica;

public interface ScenarioContext {

    /**
     * Adds the provided {@link Object} value to the provided key.
     *
     * <p>
     * If the storage already has a value under the provided key the value will be overwritten.
     * </p>
     *
     * @param key the key under which the value will be stored
     * @param value the value
     */
    public void add(String key, Object value);


    /**
     * Retriving value from storage stored under the provided key.
     *
     * @param key the key
     * @return {@link Object} value
     */
    public Object get(String key);
}

