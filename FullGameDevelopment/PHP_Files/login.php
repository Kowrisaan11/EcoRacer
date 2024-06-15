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
    $namecheckquery = "SELECT username, salt, hash, score FROM players WHERE username='" . mysqli_real_escape_string($con, $username) . "';";

    $namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed"); // Error code #2: Name check query failed


    if (mysqli_num_rows($namecheck) != 1){

    	echo "5: Either no user with name, or more than one"; //error code #5 - number of names matching != 1
    	exit();
    }

    //get login info from query
    $existinginfo = mysqli_fetch_assoc($namecheck);
    $salt = $existinginfo["salt"];
    $hash = $existinginfo["hash"];


    $loginhash = crypt($password, $salt);
    if($hash != $loginhash){
    	echo "6: Incorrect password";//error code 06 - password does not hash to match table
    	exit();
    }

    echo "0\t" . $existinginfo["score"];

?>