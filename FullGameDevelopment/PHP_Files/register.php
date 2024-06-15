<?php
    // Allow from any origin
    header("Access-Control-Allow-Origin: *");
    header("Access-Control-Allow-Methods: POST, GET, OPTIONS");
    header("Access-Control-Allow-Headers: X-Requested-With, Content-Type");
    
	// MySQL connection parameters (using environment variables)
    $servername = getenv('MYSQL_HOST');
    $username = getenv('MYSQL_USER');
    $password = getenv('MYSQL_PASSWORD');
    $dbname = getenv('MYSQL_DATABASE');

    // Create connection
    $con = mysqli_connect($servername, $username, $password, $dbname);

    // Check connection
    if (mysqli_connect_errno()) {
        echo "1: Connection failed"; // Error code #1: Connection failed
        exit();
    }

    // Get POST data
    $username = $_POST["name"];
    $password = $_POST["password"];

    // Check if username exists
    $namecheckquery = "SELECT username FROM players WHERE username='" . mysqli_real_escape_string($con, $username) . "';";

    $namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed"); // Error code #2: Name check query failed

    if (mysqli_num_rows($namecheck) > 0) {
        echo "3: Name already exists"; // Error code #3: Name exists, cannot register
        exit();
    }

    // Create salt and hash for password
    $salt = "\$5\$round=5000\$" . "steamedhams" . $username . "\$";
    $hash = crypt($password, $salt);

    // Insert user into the database
    $insertuserquery = "INSERT INTO players (username, hash, salt) VALUES('" . mysqli_real_escape_string($con, $username) . "','" . mysqli_real_escape_string($con, $hash) . "', '" . mysqli_real_escape_string($con, $salt) . "');";

    mysqli_query($con, $insertuserquery) or die("4: Insert player query failed"); // Error code #4: Insert query failed

    echo "0"; // Success
?>
