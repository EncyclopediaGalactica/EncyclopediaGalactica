package com.encyclopediagalactica.utils;

import org.springframework.stereotype.Service;

@Service
public class InputStringUtilsImpl implements InputStringUtils {

    @Override
    public String provideValue(String input) {
        return switch (input) {
            case "<null>" -> null;
            case "<3spaces>" -> "   ";
            case "<empty>" -> "";
            default -> input;
        };
    }
}
