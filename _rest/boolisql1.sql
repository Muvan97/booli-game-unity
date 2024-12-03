-- phpMyAdmin SQL Dump
-- version 5.2.1deb3
-- https://www.phpmyadmin.net/
--
-- Хост: localhost:3306
-- Время создания: Сен 17 2024 г., 12:48
-- Версия сервера: 8.0.39-0ubuntu0.24.04.2
-- Версия PHP: 8.3.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `boolisql1`
--

-- --------------------------------------------------------

--
-- Структура таблицы `players`
--

CREATE TABLE `players` (
  `id` int UNSIGNED NOT NULL,
  `identifier` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `refferal_identifier` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `game_data` varchar(255) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `invited_players` text COLLATE utf8mb4_unicode_520_ci,
  `receiving_time_of_last_referral_bonus` datetime DEFAULT NULL,
  `bonus_in_booli_for_refferal` int UNSIGNED DEFAULT NULL,
  `bonus_in_coins_for_refferal` int UNSIGNED DEFAULT NULL,
  `number_opened_levels_for_give_bonus_for_refferal` int UNSIGNED DEFAULT NULL,
  `booli_withdraw_information` varchar(191) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

--
-- Дамп данных таблицы `players`
--

INSERT INTO `players` (`id`, `identifier`, `refferal_identifier`, `game_data`, `invited_players`, `receiving_time_of_last_referral_bonus`, `bonus_in_booli_for_refferal`, `bonus_in_coins_for_refferal`, `number_opened_levels_for_give_bonus_for_refferal`, `booli_withdraw_information`) VALUES
(51, '5931302254 ', '1371948480', '{\"TowerUpgradeData\":[{\"Level\":3,\"TowerIndex\":0},{\"Level\":4,\"TowerIndex\":1},{\"Level\":3,\"TowerIndex\":2},{\"Level\":2,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"911\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":10}', NULL, NULL, NULL, NULL, NULL, NULL),
(52, '50007584', '1371948480', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"500\"},\"OpenLevelIndex\":0}', NULL, NULL, NULL, NULL, NULL, NULL),
(54, '272401691', '1', '{\"TowerUpgradeData\":[{\"Level\":3,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":1,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"205\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":3}', NULL, NULL, NULL, NULL, NULL, NULL),
(55, '6847407378', '1', '{\"TowerUpgradeData\":[{\"Level\":3,\"TowerIndex\":0},{\"Level\":3,\"TowerIndex\":1},{\"Level\":2,\"TowerIndex\":2},{\"Level\":3,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"1565\"},\"OpenLevelIndex\":8}', '{\"285102970\":400}', '2024-09-06 07:22:33', NULL, NULL, NULL, NULL),
(56, '1', '3', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":1,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"2359.8\"},\"OpenLevelIndex\":0}', '{\"509552587\":214,\"545921\":120,\"1385948489\":200,\"1471580467\":200,\"7079987065\":285.8,\"965155467\":216,\"354707848\":200,\"530830052\":\"100\",\"1941691189\":\"100\",\"430371851\":\"100\",\"123873105\":\"100\",\"799939264\":\"100\",\"256447900\":\"100\",\"1643891756\":\"100\",\"5042440556\":\"100\",\"2222\":{\"NumberCoins\":\"0\",\"NumberBooli\":\"0\"},\"1000\":{\"NumberCoins\":\"0\",\"NumberBooli\":\"0\"},\"7079987066\":{\"id\":\"0\",\"number_coins\":\"0\",\"number_booli\":\"0\",\"5507141351\":\"5507141351\"},\"7079987067\":{\"id\":\"5507141351\",\"NumberGiftedCoins\":\"0\",\"NumberGiftedBooli\":\"0\"},\"7079987068\":{\"id\":\"945369548\",\"NumberGiftedCoins\":\"0\",\"NumberGiftedBooli\":\"0\"},\"7079987069\":{\"id\":\"620678672\",\"NumberGiftedCoins\":\"0\",\"NumberGiftedBooli\":\"0\"}}', '2024-09-06 07:26:11', NULL, NULL, NULL, NULL),
(57, '285102970', '6847407378', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"500\"},\"OpenLevelIndex\":0}', NULL, NULL, NULL, NULL, NULL, NULL),
(58, '509552587', '1', '{\"TowerUpgradeData\":[{\"Level\":3,\"TowerIndex\":0},{\"Level\":3,\"TowerIndex\":1},{\"Level\":3,\"TowerIndex\":2},{\"Level\":2,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"463\",\"NumberBooli\":\"5300\"},\"OpenLevelIndex\":5}', '[{\"id\":\"883060779\",\"NumberGiftedCoins\":\"2000\",\"NumberGiftedBooli\":\"300\"},{\"id\":\"1106160502\",\"NumberGiftedCoins\":\"2000\",\"NumberGiftedBooli\":\"300\"}]', NULL, NULL, NULL, NULL, NULL),
(59, '545921', '1', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":1,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":1,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"490\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":1}', NULL, NULL, NULL, NULL, NULL, NULL),
(60, '1385948489', '1', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"500\"},\"OpenLevelIndex\":0}', NULL, NULL, NULL, NULL, NULL, NULL),
(61, '1471580467', '1', '{\"TowerUpgradeData\":[{\"Level\":1,\"TowerIndex\":0},{\"Level\":1,\"TowerIndex\":1},{\"Level\":2,\"TowerIndex\":2},{\"Level\":2,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"229\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":1}', NULL, NULL, NULL, NULL, NULL, NULL),
(62, '7079987065', '1', '{\"TowerUpgradeData\":[{\"Level\":3,\"TowerIndex\":0},{\"Level\":3,\"TowerIndex\":1},{\"Level\":3,\"TowerIndex\":2},{\"Level\":3,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"1681\"},\"OpenLevelIndex\":9}', NULL, NULL, NULL, NULL, NULL, NULL),
(63, '965155467', '1', '{\"TowerUpgradeData\":[{\"Level\":4,\"TowerIndex\":0},{\"Level\":3,\"TowerIndex\":1},{\"Level\":3,\"TowerIndex\":2},{\"Level\":3,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"561\"},\"OpenLevelIndex\":9}', NULL, NULL, NULL, NULL, NULL, NULL),
(64, '354707848', '1', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"500\"},\"OpenLevelIndex\":0}', NULL, NULL, NULL, NULL, NULL, NULL),
(65, '2', '4', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"500\"},\"OpenLevelIndex\":0}', NULL, NULL, NULL, NULL, NULL, NULL),
(67, '530830052', '1', '{\"TowerUpgradeData\":[{\"Level\":5,\"TowerIndex\":0},{\"Level\":6,\"TowerIndex\":1},{\"Level\":6,\"TowerIndex\":2},{\"Level\":5,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"2045\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":23}', NULL, NULL, NULL, NULL, NULL, NULL),
(68, '1371948480', 'null', '{\"TowerUpgradeData\":[{\"Level\":1,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"300\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, NULL, NULL, NULL, '{\"wallet_address\":\"EQBfVMjLKFKzQANWYOo_FbqKoFB_9Wfen3Dd9CZTnBfo7U3v\",\"number_sent_booli\":\"12\"}'),
(69, '682321044', 'null', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"500\"},\"OpenLevelIndex\":0}', NULL, NULL, NULL, NULL, NULL, NULL),
(70, '1941691189', '1', '{\"TowerUpgradeData\":[{\"Level\":4,\"TowerIndex\":0},{\"Level\":3,\"TowerIndex\":1},{\"Level\":3,\"TowerIndex\":2},{\"Level\":3,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"237\"},\"OpenLevelIndex\":9}', NULL, NULL, NULL, NULL, NULL, NULL),
(71, '430371851', '1', '{\"TowerUpgradeData\":[{\"Level\":1,\"TowerIndex\":0},{\"Level\":4,\"TowerIndex\":1},{\"Level\":4,\"TowerIndex\":2},{\"Level\":3,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"95\"},\"OpenLevelIndex\":9}', NULL, NULL, NULL, NULL, NULL, NULL),
(72, '123873105', '1', '{\"TowerUpgradeData\":[{\"Level\":1,\"TowerIndex\":0},{\"Level\":1,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"100\"},\"OpenLevelIndex\":0}', NULL, NULL, NULL, NULL, NULL, NULL),
(73, '799939264', '1', '{\"TowerUpgradeData\":[{\"Level\":3,\"TowerIndex\":0},{\"Level\":4,\"TowerIndex\":1},{\"Level\":3,\"TowerIndex\":2},{\"Level\":3,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"966\"},\"OpenLevelIndex\":10}', NULL, NULL, NULL, NULL, NULL, NULL),
(74, '1854027087', 'null', '{\"TowerUpgradeData\":[{\"Level\":8,\"TowerIndex\":0},{\"Level\":9,\"TowerIndex\":1},{\"Level\":8,\"TowerIndex\":2},{\"Level\":8,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"4334\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":33}', NULL, NULL, NULL, NULL, NULL, NULL),
(75, '256447900', '1', '{\"TowerUpgradeData\":[{\"Level\":1,\"TowerIndex\":0},{\"Level\":1,\"TowerIndex\":1},{\"Level\":1,\"TowerIndex\":2},{\"Level\":1,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"190\"},\"OpenLevelIndex\":1}', NULL, NULL, NULL, NULL, NULL, NULL),
(76, '22191004', 'null', '{\"TowerUpgradeData\":[{\"Level\":2,\"TowerIndex\":0},{\"Level\":3,\"TowerIndex\":1},{\"Level\":4,\"TowerIndex\":2},{\"Level\":3,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"966\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":10}', NULL, NULL, NULL, NULL, NULL, NULL),
(77, '1643891756', '1', '{\"TowerUpgradeData\":[{\"Level\":3,\"TowerIndex\":0},{\"Level\":3,\"TowerIndex\":1},{\"Level\":3,\"TowerIndex\":2},{\"Level\":3,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberMoney\":\"13\"},\"OpenLevelIndex\":7}', NULL, NULL, NULL, NULL, NULL, NULL),
(78, '5042440556', '1', '{\"TowerUpgradeData\":[{\"Level\":3,\"TowerIndex\":0},{\"Level\":4,\"TowerIndex\":1},{\"Level\":5,\"TowerIndex\":2},{\"Level\":7,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"5287\",\"NumberBooli\":\"5000\"},\"OpenLevelIndex\":16}', NULL, NULL, NULL, NULL, NULL, NULL),
(79, '22', 'null', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\",\"NumberMoney\":\"0\"},\"OpenLevelIndex\":0}', '{\"11\":null}', NULL, 0, 0, 0, NULL),
(80, '11', '22', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, 0, 0, 0, NULL),
(81, '001', 'null', '{\"TowerUpgradeData\":[],\"CurrenciesData\":{\"NumberCoins\":\"0\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, 0, 0, 0, NULL),
(89, '123', 'n', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, 0, 0, 0, NULL),
(121, '1000', '1000', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', '{\"0\":{\"id\":\"2000\",\"NumberGiftedCoins\":\"0\",\"NumberGiftedBooli\":\"0\"},\"1000\":{\"NumberGiftedCoins\":null,\"NumberGiftedBooli\":null}}', NULL, 300, 2000, 2, NULL),
(122, '2000', '1000', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, 300, 2000, 2, NULL),
(123, '3000', '3000', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, 300, 2000, 2, NULL),
(124, '5000', '5000', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', '{\"0\":{\"id\":\"6000\",\"NumberGiftedCoins\":\"0\",\"NumberGiftedBooli\":\"0\"},\"5000\":{\"NumberGiftedCoins\":null,\"NumberGiftedBooli\":null}}', NULL, 300, 2000, 2, NULL),
(125, '6000', '5000', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, 300, 2000, 2, NULL),
(126, '7000', '7000', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', '{\"0\":{\"id\":\"8000\",\"NumberGiftedCoins\":\"0\",\"NumberGiftedBooli\":\"0\"},\"7000\":{\"NumberGiftedCoins\":null,\"NumberGiftedBooli\":null}}', NULL, 300, 2000, 2, NULL),
(127, '8000', '7000', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, 300, 2000, 2, NULL),
(128, '9000', '9000', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', '{\"0\":{\"id\":\"10000\",\"NumberGiftedCoins\":\"0\",\"NumberGiftedBooli\":\"0\"},\"9000\":{\"NumberGiftedCoins\":null,\"NumberGiftedBooli\":null}}', NULL, 300, 2000, 2, NULL),
(129, '10000', '9000', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, 300, 2000, 2, NULL),
(130, '12000', '12000', '{\"TowerUpgradeData\":[],\"CurrenciesData\":{\"NumberCoins\":\"0\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', '{\"0\":{\"id\":\"13000\",\"NumberGiftedCoins\":\"0\",\"NumberGiftedBooli\":\"0\"},\"12000\":{\"NumberGiftedCoins\":null,\"NumberGiftedBooli\":null}}', NULL, 300, 2000, 2, NULL),
(131, '13000', '12000', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, 300, 2000, 2, NULL),
(132, '15000', '15000', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', '{\"0\":{\"id\":\"15555\",\"NumberGiftedCoins\":null,\"NumberGiftedBooli\":null},\"15000\":{\"NumberGiftedCoins\":null,\"NumberGiftedBooli\":null}}', NULL, 300, 2000, 2, NULL),
(133, '15555', '15000', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, 300, 2000, 2, NULL),
(134, '25000', '25000', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"4500\",\"NumberBooli\":\"600\"},\"OpenLevelIndex\":0}', '[{\"id\":\"26000\",\"NumberGiftedCoins\":\"2000\",\"NumberGiftedBooli\":\"300\"},{\"id\":\"123213\",\"NumberGiftedCoins\":\"2000\",\"NumberGiftedBooli\":\"300\"}]', NULL, NULL, NULL, NULL, NULL),
(135, '26000', '25000', '{\"TowerUpgradeData\":[{\"Level\":2,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"970\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":2}', NULL, NULL, NULL, NULL, NULL, NULL),
(136, '123213', '25000', '{\"TowerUpgradeData\":[{\"Level\":1,\"TowerIndex\":0},{\"Level\":1,\"TowerIndex\":1},{\"Level\":1,\"TowerIndex\":2},{\"Level\":1,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"1283\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":4}', NULL, NULL, NULL, NULL, NULL, NULL),
(137, '883060779', '509552587', '{\"TowerUpgradeData\":[{\"Level\":6,\"TowerIndex\":0},{\"Level\":7,\"TowerIndex\":1},{\"Level\":6,\"TowerIndex\":2},{\"Level\":7,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"6292\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":26}', NULL, NULL, NULL, NULL, NULL, NULL),
(138, '736456922', 'null', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, NULL, NULL, NULL, NULL),
(139, '6027937020', 'null', '{\"TowerUpgradeData\":[{\"Level\":3,\"TowerIndex\":0},{\"Level\":3,\"TowerIndex\":1},{\"Level\":2,\"TowerIndex\":2},{\"Level\":4,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"49\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":8}', NULL, NULL, NULL, NULL, NULL, NULL),
(140, '1106160502', '509552587', '{\"TowerUpgradeData\":[{\"Level\":3,\"TowerIndex\":0},{\"Level\":4,\"TowerIndex\":1},{\"Level\":3,\"TowerIndex\":2},{\"Level\":3,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"1005\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":11}', NULL, NULL, NULL, NULL, NULL, NULL),
(141, '6422235070', 'null', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":1,\"TowerIndex\":2},{\"Level\":1,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"100\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, NULL, NULL, NULL, NULL),
(142, '5507141351', '1', '{\"TowerUpgradeData\":[{\"Level\":4,\"TowerIndex\":0},{\"Level\":4,\"TowerIndex\":1},{\"Level\":3,\"TowerIndex\":2},{\"Level\":5,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"163\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":13}', NULL, NULL, NULL, NULL, NULL, NULL),
(143, '474190783', 'null', '{\"TowerUpgradeData\":[{\"Level\":2,\"TowerIndex\":0},{\"Level\":2,\"TowerIndex\":1},{\"Level\":1,\"TowerIndex\":2},{\"Level\":2,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"40\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":3}', NULL, NULL, NULL, NULL, NULL, NULL),
(144, '945369548', '1', '{\"TowerUpgradeData\":[{\"Level\":0,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"500\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, NULL, NULL, NULL, NULL),
(145, '620678672', '1', '{\"TowerUpgradeData\":[{\"Level\":1,\"TowerIndex\":0},{\"Level\":1,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"100\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, NULL, NULL, NULL, NULL),
(146, '483683452', 'null', '{\"TowerUpgradeData\":[{\"Level\":1,\"TowerIndex\":0},{\"Level\":0,\"TowerIndex\":1},{\"Level\":0,\"TowerIndex\":2},{\"Level\":0,\"TowerIndex\":3}],\"CurrenciesData\":{\"NumberCoins\":\"300\",\"NumberBooli\":\"0\"},\"OpenLevelIndex\":0}', NULL, NULL, NULL, NULL, NULL, NULL);

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `players`
--
ALTER TABLE `players`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `players`
--
ALTER TABLE `players`
  MODIFY `id` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=147;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
