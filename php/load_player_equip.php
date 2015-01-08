<?php 
$tabelle="equ";
$where_string='player_id="'.$_POST['player_id'].'"';

include ("db.php");
$data=$datenbank->sql_select($tabelle,"*", $where_string);
foreach ($data as $key => $value) {
	echo $value['item_name'].";\n";
}
?>