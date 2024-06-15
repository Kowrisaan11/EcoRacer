package com.ecoRacer.quizApp.service;

import com.ecoRacer.quizApp.model.User;
import com.ecoRacer.quizApp.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service; // Import the Service annotation

import java.util.List;

@Service 
public class UserServiceImplementation implements UserService {

    @Autowired
    private UserRepository userRepository;

    @Override
    public User saveUser(User user) {
        return userRepository.save(user);

    }

    @Override
    public List<User> getAllUsers(){
        return userRepository.findAll();
    }

    @Override
    public User getLastAddedUser() {
        return userRepository.findTopByOrderByIdDesc();
    }
}

