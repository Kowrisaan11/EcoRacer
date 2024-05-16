package com.ecoRacer.quizApp.service;

import com.ecoRacer.quizApp.model.User;

import java.util.List;

public interface UserService {

    public User saveUser(User user);
    public List<User> getAllUsers();
}
