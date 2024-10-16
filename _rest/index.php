<?php

require "./libs/rb-mysql.php";
require "database.php";

R::setup('mysql:host=localhost; dbname=boolisql', 'boolisql', '');
header("Access-Control-Allow-Origin: *"); // Разрешить запросы с любых доменов. Можно указать конкретный домен вместо "*"
header("Access-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS"); // Разрешить конкретные методы запросов
header("Access-Control-Allow-Headers: Content-Type, Authorization"); // Разрешить определенные заголовки, которые будут отправлены на сервер

if (!R::testConnection())
{
    echo json_encode(array("Error" => "ConnectionError"));
    exit();
}

$bot_token = "TOKEN";

if (!empty($_POST["invoice_data"]))
{
    echo send_invoice_request($bot_token);

    exit();
}

$identifier = $_POST["identifier"];

if (empty($identifier))
{
    echo json_encode(array("Error" => "IdentifierIsEmpty"));
    exit();
}

$user = get_player_account_by_identifier($identifier);

if (!empty($_POST["player_account_data"]))
{
    $player_account_data = $_POST["player_account_data"];
    $player_account_data = json_decode($player_account_data, true);

if (!empty($player_account_data["booli_withdraw_information"]))
{
    save_player_account_data($user, $player_account_data, true);  

    exit();
}
}
if ($user === null)
{
    try_register_user($identifier, $player_account_data, $is_registered);
    echo get_player_account_data_in_json_format($user); 
    exit();
}

if (!empty($_POST["is_only_save_data"]))
{
    save_player_account_data($user, $player_account_data, true);  

    exit();
}
else if (!empty($_POST["is_get_only_avatar_and_nickname"]) && $_POST["is_get_only_avatar_and_nickname"])
{
    echo json_encode(get_avatar_and_nickname_by_identifier($identifier, $bot_token));
}
else
{
    //try_give_reward_refferals($user);
    increase_player_balance_for_refferals($user);
    echo get_player_account_data_in_json_format($user);   
}


function get_player_account_data_in_json_format($user)
{
    return json_encode(array("GameData" => $user->game_data ?? "null",
                             "InvitedPlayers" => $user->invited_players ?? "null", 
                            "IsPlayerSentBooli" => json_encode(!empty($user->booli_withdraw_information))));
}

function send_invoice_request($bot_token)
{
    $invoice_data = json_decode($_POST["invoice_data"], true);
    $product_data = get_product_data($invoice_data);

    $link = "https://api.telegram.org/bot$bot_token/createInvoiceLink";

    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL, $link);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($ch, CURLOPT_POST, 1);
    // Edit: prior variable $postFields should be $postfields;
    curl_setopt($ch, CURLOPT_POSTFIELDS, $product_data);
    $result = json_decode(curl_exec($ch), true);

    if ($result["ok"] === false)
        return json_encode(array("Error" => $result["description"]));

    return json_encode(array("InvoiceLink" => stripcslashes($result["result"])));
}
function get_product_data($invoice_data) 
{
    $title = $invoice_data['title'];
    $description = $invoice_data['description'];
    $payload = $invoice_data['payload'];
    $providerToken = $invoice_data['provider_token'];
    $currency = $invoice_data['currency'];
    $amount = $invoice_data['amount'];

    $data = 
    [
        'title' => $title,
        'description' => $description,
        'payload' => $payload,
        'provider_token' => $providerToken,
        'currency' => $currency,
        'prices' => json_encode([
            [
                'label' => $title,
                'amount' => $amount
            ]
        ])
    ];

    return $data;
}


function get_avatar_and_nickname_by_identifier($identifier, $bot_token)
{
    $bot_url = "https://api.telegram.org/bot$bot_token/";
    $url = $bot_url."getChat?chat_id=$identifier";
    
    $text = file_get_contents($url);

    $user_data = json_decode($text, true);

    $small_file_id = $user_data["result"]["photo"]["small_file_id"];
    $bigFileUrl = $bot_url."getFile?file_id=$small_file_id";

    $bigFileText = file_get_contents($bigFileUrl);

    $filePath = json_decode($bigFileText, true)["result"]["file_path"];
    $photo_url = "https://api.telegram.org/file/bot$bot_token/".$filePath;

    $base64_image = base64_encode(file_get_contents($photo_url));

    $username = (string) $user_data["result"]["username"];

    return array("Avatar" => $base64_image, "Nickname" => $username);
}
?>