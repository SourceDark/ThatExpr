<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;

use App\Http\Requests;
use DB;

class ExprApiController extends Controller
{
    public static function getAllExprByUserId($userId)
    {
        $tags = DB::table('tag')->where('owner', '=', $userId)->get();
        $exprIds = [];
        foreach ($tags as $tag) {
            array_push($exprIds, $tag->expr_id);
        }
        $exprs = DB::table('expr')->whereIn('id', $exprIds)->get();
        $exprMap = [];
        foreach ($exprs as $expr) {
            $exprMap[$expr->id] = $expr;
            $expr->tags = [];
        }
        foreach ($tags as $tag) {
            array_push($exprMap[$tag->expr_id]->tags, $tag);
        }
        return $exprs;
    }
}
