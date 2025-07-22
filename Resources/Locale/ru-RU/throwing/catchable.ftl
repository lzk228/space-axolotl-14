catchable-component-success-self = Вы ловите { $item }!
catchable-component-success-others = { CAPITALIZE($catcher) } ловит { $item }!
catchable-component-fail-self = Вы не смогли словить { $item }!
catchable-component-fail-others = { CAPITALIZE($catcher) } не { GENDER($catcher) ->
        [male] смог
        [female] смогла
        [epicene] смогло
       *[neuter] смогли
        } поймать { $item }!
