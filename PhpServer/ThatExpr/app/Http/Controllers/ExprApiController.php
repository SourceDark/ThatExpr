<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;

use App\Http\Requests;
use DB;

class ExprApiController extends Controller
{
    public static function index()
    {
        $users = DB::table('expr')->get();

        return ['users' => $users];
    }
}
