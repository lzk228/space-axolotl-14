## Survivor

roles-antag-survivor-name = Выживший
# It's a Halo reference
roles-antag-survivor-objective = Текущая задача: Выжить
survivor-role-greeting =
    Вы - выживший.
    Ваша главная задача остаться в живых и вернуться на Центком.
    Соберите столько огневой мощи, сколько необходимо для гарантии вашего выживания.
    Никому не доверяйте.
survivor-round-end-dead-count =
    { $deadCount ->
        [one] [color=red]{ $deadCount }[/color] выживший умер.
       *[other] [color=red]{ $deadCount }[/color] выживших умерло.
    }
survivor-round-end-alive-count =
    { $aliveCount ->
        [one] [color=yellow]{ $aliveCount }[/color] выживший остался на станции.
       *[other] [color=yellow]{ $aliveCount }[/color] выживших осталось на станции.
    }
survivor-round-end-alive-on-shuttle-count =
    { $aliveCount ->
        [one] [color=green]{ $aliveCount }[/color] выживший выбрался живым.
       *[other] [color=green]{ $aliveCount }[/color] выживших выбралось живыми.
    }

## Wizard

objective-issuer-swf = [color=turquoise]Федерация космиеских волшебников[/color]
wizard-title = Волшебник
wizard-description = На станции есть волшебник! Вы никогда не знаете, что он может сделать.
roles-antag-wizard-name = Волшебник
roles-antag-wizard-objective = Преподайте им урок, который они никогда не забудут.
wizard-role-greeting =
    ВЫ ВОЛШЕБНИК!
    Между Федерацией космических волшебников и Nanotrasen возникли противоречия.
    Поэтому Федерация космических волшебников выбрала вас, чтобы нанести визит на станцию.
    Продемонстрируйте им свои способности.
    Что вы будете делать, зависит от вас, но помните, что Космические волшебники хотят, чтобы вы выбрались живыми.
wizard-round-end-name = волшебник

## TODO: Wizard Apprentice (Coming sometime post-wizard release)

