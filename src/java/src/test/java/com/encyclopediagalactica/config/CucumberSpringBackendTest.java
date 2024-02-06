package com.encyclopediagalactica.config;

import io.cucumber.spring.CucumberContextConfiguration;
import org.junit.platform.suite.api.SelectClasspathResource;
import org.junit.platform.suite.api.Suite;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.DirtiesContext;

@Suite
@CucumberContextConfiguration
@SelectClasspathResource("features")
@SpringBootTest
@DirtiesContext
public class CucumberSpringBackendTest {
}
