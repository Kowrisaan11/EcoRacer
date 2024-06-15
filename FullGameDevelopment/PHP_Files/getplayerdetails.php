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
    echo json_encode(["error" => "1: Connection failed"]); // Error code #1: Connection failed
    exit();
}

// Fetch player details
$query = "SELECT username, score FROM players";
$result = mysqli_query($con, $query) or die(json_encode(["error" => "2: Query failed"])); // Error code #2: Query failed

$players = [];

while ($row = mysqli_fetch_assoc($result)) {
    $players[] = $row;
}

echo json_encode(["players" => $players]);

?>
