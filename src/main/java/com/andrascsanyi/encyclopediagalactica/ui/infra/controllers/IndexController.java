package com.andrascsanyi.encyclopediagalactica.ui.infra.controllers;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
@RequestMapping(value = "/")
public class IndexController {

    @GetMapping
    public String getMainPage(Model model) {

        return "index.html";
    }

}