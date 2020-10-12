function Get-TwitterMutedUser {
    [CmdletBinding(DefaultParameterSetName='Default')]
    param(
        [Parameter(ParameterSetName='List')]
        [switch]$List,

        [Parameter(ParameterSetName='List')]
        [switch]$SkipStatus,

        [Parameter(ParameterSetName='List')]
        [switch]$ExcludeEntities
    )

    if ($PSCmdlet.ParameterSetName -eq 'List') {
        $Query = [hashtable]::new()
        if ($PSBoundParameters.ContainsKey('SkipStatus')) {
            $Query.Add('skip_status','true')
        }
        if ($PSBoundParameters.ContainsKey('ExcludeEntities')) {
            $Query.Add('include_entities','false')
        } else {
            $Query.Add('include_entities','true')
        }

        $OAuthParameters = [OAuthParameters]::new(
            'Get',
            'https://api.twitter.com/1.1/mutes/users/list.json',
            $Query
        )
        Invoke-TwitterCursorRequest -OAuthParameters $OAuthParameters -ReturnValue users

    } else {
        $Query = [hashtable]::new()
        $Query.Add('cursor',-1)

        $OAuthParameters = [OAuthParameters]::new(
            'Get',
            'https://api.twitter.com/1.1/mutes/users/ids.json',
            $Query
        )
        Invoke-TwitterCursorRequest -OAuthParameters $OAuthParameters -ReturnValue ids
    }
}
