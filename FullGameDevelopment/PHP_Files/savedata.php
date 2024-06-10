<?php

// MySQL connection parameters
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "unity";

// Create connection
$con = mysqli_connect($servername, $username, $password, $dbname);

// Check connection
if (mysqli_connect_errno()) {
    echo "1: Connection failed"; // Error code #1: Connection failed
    exit();
}

$username  = $_POST["name"];
$newscore = $_POST["score"];

// Double check there is only one user with this name
$namecheckquery = "SELECT username FROM players WHERE username='" . mysqli_real_escape_string($con, $username) . "';";

$namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed"); // Error code #2: Name check query failed
if (mysqli_num_rows($namecheck) != 1) {
    echo "5: Either no user with name, or more than one"; // Error code #5: Number of names matching != 1
    exit();
}

// Prepare the new score for the update query
$newscore = mysqli_real_escape_string($con, $newscore);

$updatequery = "UPDATE players SET score = " . $newscore . " WHERE username = '" . mysqli_real_escape_string($con, $username) . "';";
mysqli_query($con, $updatequery) or die("7: Save query failed"); // Error code #7: UPDATE query failed

echo "0";

?>
