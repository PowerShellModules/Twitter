---
external help file: BluebirdPS-help.xml
Module Name: BluebirdPS
online version: https://bluebirdps.anovelidea.org/en/latest/Get-TwitterTimeline
schema: 2.0.0
---

# Get-TwitterTimeline

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### Mentions
```
Get-TwitterTimeline [-MentionsTimeline] [<CommonParameters>]
```

### Home
```
Get-TwitterTimeline [-HomeTimeline] [<CommonParameters>]
```

### User
```
Get-TwitterTimeline [-ScreenName <String>] [-UserId <String>] [-ExcludeRetweets] [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -ExcludeRetweets
{{ Fill ExcludeRetweets Description }}

```yaml
Type: SwitchParameter
Parameter Sets: User
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HomeTimeline
{{ Fill HomeTimeline Description }}

```yaml
Type: SwitchParameter
Parameter Sets: Home
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MentionsTimeline
{{ Fill MentionsTimeline Description }}

```yaml
Type: SwitchParameter
Parameter Sets: Mentions
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ScreenName

The screen name of the user for whom to return results.

```yaml
Type: String
Parameter Sets: User
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserId

The ID of the user for whom to return results.

```yaml
Type: String
Parameter Sets: User
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Object

## NOTES

## RELATED LINKS

[Online Version](https://bluebirdps.anovelidea.org/en/latest/Get-TwitterTimeline)
