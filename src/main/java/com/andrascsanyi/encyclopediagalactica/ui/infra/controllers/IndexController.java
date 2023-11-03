package com.andrascsanyi.encyclopediagalactica.ui.infra.controllers;

import com.andrascsanyi.encyclopediagalactica.iam.application.GetModulesScenario;
import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleOutput;
import java.util.List;
import lombok.NonNull;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
@RequestMapping(value = "/")
public class IndexController {

    private final GetModulesScenario getModulesScenario;
    private final static Logger log = LoggerFactory.getLogger(IndexController.class);

    public IndexController(
        @NonNull GetModulesScenario getModulesScenario) {
        this.getModulesScenario = getModulesScenario;
    }

    @GetMapping("/")
    public String getMainPage(Model model) {
        String message = IndexController.class.getName() + "getMainPage() is called.";
        log.info(message);
        return "index.html";
    }

    @GetMapping("/fragments/header")
    public String getPageHeader(Model model) {
        
        String message = IndexController.class.getName() + "getPAgeHeader() is called.";
        log.info(message);

        List<ModuleOutput> modules = getModulesScenario.getModules();
        model.addAttribute("moduleList", modules);
        return "fragments/header.html";
    }

}
