<?php
require_once("./libs/rb-mysql.php");
function is_account_contains_in_date_base($identifier)
{
    return get_player_account_by_identifier($identifier) !== null;
}


function get_player_account_by_identifier($identifier)
{
    return R::findOne('players', "identifier = ?", [ $identifier, ]);
}

function save_player_account_data($user, $player_account_data, $isEcho = false)
{
    if (!empty($player_account_data))
    {
        foreach ($player_account_data as $table => $value)
            $user->$table = $value;
    }

    R::store($user);  

    if ($isEcho)
        echo json_encode(array("IsSaved" => true));
}

function register_in_refferal_system($identifier, $refferal_identifier)
{
    if (!empty($refferal_identifier) && $refferal_identifier != "null" && $refferal_identifier != $identifier)
    {
        $inviting_player = get_player_account_by_identifier($refferal_identifier);

        if (!empty($inviting_player))
        {
            $invited_players = $inviting_player->invited_players;

            if ($invited_players != null)
            {
                $invited_players = json_decode($invited_players, true);
                array_push($invited_players, new FriendData($identifier));
            }
            else 
            {
                $invited_players = array(new FriendData($identifier));
            }

            $inviting_player_game_data = get_player_game_data($inviting_player);
            //increase_player_balance($_POST["bonus_for_refferal"], $inviting_player_game_data);
            $invited_player_account_data = array("invited_players" => json_encode($invited_players),
            "game_data" => json_encode($inviting_player_game_data));

            save_player_account_data($inviting_player, $invited_player_account_data, false);
        }
    }
}

class FriendData
{
    public $id = "0";
    public $NumberGiftedCoins = "0";
    public $NumberGiftedBooli = "0";

    function __construct($id)
    {
        $this->id = $id;
    }
}

function try_give_reward_refferals($player) // old
{
    if (empty($player->invited_players))
        return;

    $time_before_giving_reward_for_refferal = $_POST["time_before_giving_reward_for_refferal"];
    $receiving_time_of_last_referral_bonus = $player->receiving_time_of_last_referral_bonus;
    $now = strtotime(date("Y-m-d H:i:s"));

    if ($receiving_time_of_last_referral_bonus == null || (($now - (strtotime($receiving_time_of_last_referral_bonus))) / 60 >= $time_before_giving_reward_for_refferal))
    {
        $player->receiving_time_of_last_referral_bonus = date("Y-m-d H:i:s");
    }
    else
    {
        return;
    }

    $invited_players = json_decode($player->invited_players, true);

    $number_money_as_bonus = 0;
    foreach ($invited_players as $listIndex => $numberGiftedCurrencies) 
    {
        $invited_player = get_player_account_by_identifier($listIndex);
        $invited_player_game_data = get_player_game_data($invited_player);
        $invited_player_number_money = get_player_number_currency($invited_player_game_data, "Coins");
        $bonus = (float) $invited_player_number_money * ((float) $_POST["percentage_refferal_balance_as_bonus"] / 100);
        $invited_players[$listIndex] += $bonus;
        $number_money_as_bonus += $bonus;
    }

    $player_game_data = get_player_game_data($player);
    increase_player_balance("Coins", $number_money_as_bonus, $player_game_data);
    $player_account_data = array("game_data" => json_encode($player_game_data), "invited_players" => json_encode($invited_players)); 

    save_player_account_data($player, $player_account_data, false);
}

function get_player_number_currency($game_data, $currency)
{
    return $game_data["CurrenciesData"]["Number".$currency];
}
function get_player_game_data($player) 
{
    return json_decode($player->game_data, true);
}

function increase_player_balance($currency, $number, &$game_data)
{
    $player_number_money = get_player_number_currency($game_data, $currency);
    $game_data["CurrenciesData"]["Number".$currency] = (string)((float) $player_number_money + $number);
}

function increase_player_balance_for_refferals($player)
{
    $invited_players = json_decode($player->invited_players, true);
    $player_game_data = get_player_game_data($player);
    $bonus_in_coins_for_refferal = $_POST["bonus_in_coins_for_refferal"];
    $bonus_in_booli_for_refferal = $_POST["bonus_in_booli_for_refferal"];
    $isGetAtLeastOneGift = false;

    if ($invited_players == null)
        return;

    foreach ($invited_players as &$invited_player_data) 
    {
        $invited_player_id = $invited_player_data["id"];
        $invited_player = get_player_account_by_identifier($invited_player_id);
        $invited_player_game_data = get_player_game_data($invited_player);

        if ($invited_player_game_data["OpenLevelIndex"] >= (int) $_POST["number_opened_levels_for_give_bonus_for_refferal"] 
        && $invited_player_data["NumberGiftedCoins"] == "0" && $invited_player_data["NumberGiftedBooli"] == "0")
        {
            increase_player_balance("Coins", $bonus_in_coins_for_refferal, $player_game_data);
            increase_player_balance("Booli", $bonus_in_booli_for_refferal, $player_game_data);
            $invited_player_data["NumberGiftedCoins"] = $bonus_in_coins_for_refferal;
            $invited_player_data["NumberGiftedBooli"] = $bonus_in_booli_for_refferal;
            $isGetAtLeastOneGift = true;
        }
    }

    if ($isGetAtLeastOneGift)
    {
        $player_account_data = array("game_data" => json_encode($player_game_data), "invited_players" => json_encode($invited_players));

        save_player_account_data($player, $player_account_data);
    }
}

function try_register_user($identifier, $player_account_data, &$is_registered)
{
    if (is_account_contains_in_date_base($identifier)) 
    {
        $is_registered = false;
        return;
    }

    $user = R::dispense('players');
    $user->identifier = $identifier;
    $refferal_identifier = $player_account_data["refferal_identifier"];
    register_in_refferal_system($identifier, $refferal_identifier);
    save_player_account_data($user, $player_account_data, false);
    $is_registered = true;
}
?>