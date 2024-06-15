# FROM php:8.1-fpm  
# WORKDIR /var/www/html

# COPY . .

# # RUN apt-get update && apt-get install -y mysqli

# RUN apt-get update && \
#     apt-get install -y libmariadb-dev && \
#     docker-php-ext-install mysqli
# EXPOSE 80  

# Use the official PHP image from Docker Hub
FROM php:7.4-apache

# Install the mysqli extension
RUN docker-php-ext-install mysqli

# Copy your PHP script to the default web server directory
COPY . /var/www/html/

# Create a custom apache configuration file to set DirectoryIndex
RUN echo "DirectoryIndex login.php" >> /etc/apache2/apache2.conf

# Enable Apache rewrite module
RUN a2enmod rewrite

# Expose port 80
EXPOSE 80

