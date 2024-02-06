package com.encyclopediagalactica.utils;

public interface InputStringUtils {
    /**
     * If the provided input string matches certain patterns then it will be replaced to that.
     * <ul>
     *     <li><b>< null ></b> --> null</li>
     *     <li><b>< empty ></b> --> empty string</li>
     *     <li><b><3spaces></b> --> 3 spaces</li>
     * </ul>
     *
     * @param input the provided input
     * @return original value or replaced value
     */
    String provideValue(String input);
}
