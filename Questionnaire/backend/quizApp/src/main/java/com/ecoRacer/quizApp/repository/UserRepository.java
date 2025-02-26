package com.ecoRacer.quizApp.repository;
import com.ecoRacer.quizApp.model.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface UserRepository extends JpaRepository<User, Integer> {
    User findTopByOrderByIdDesc();

}
