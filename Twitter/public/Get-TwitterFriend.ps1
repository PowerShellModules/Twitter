function Get-TwitterFriend {
    [CmdletBinding(DefaultParameterSetName='Default')]
    param(
        [string]$ScreenName,
        [string]$UserId,
        [int]$ResultsPerPage,

        [Parameter(ParameterSetName='List')]
        [switch]$List,

        [Parameter(ParameterSetName='List')]
        [switch]$SkipStatus,

        [Parameter(ParameterSetName='List')]
        [switch]$ExcludeEntities
    )

    if ([string]::IsNullOrWhiteSpace($ScreenName) -and [string]::IsNullOrWhiteSpace($UserId)) {
        'You must supply either a ScreenName or UserId' | Write-Warning
        return
    } elseif ($ScreenName.Length -gt 0 -and $UserId.Length -gt 0) {
        'You must supply a ScreenName or a UserId, but not both' | Write-Warning
        return
    }

    if ($PSCmdlet.ParameterSetName -eq 'List') {
        if ($ResultsPerPage -and $ResultsPerPage -notin 1..200) {
            'For a list of follower objects, you can only request up to 200 user objects per page.' | Write-Warning
            return
        } else {
            $ResultsPerPage = 100
        }
    } else {
        if ($ResultsPerPage -and $ResultsPerPage -notin 1..5000) {
            'For list of follower ids, you can only request up to 5000 user is per page.' | Write-Warning
            return
        } else {
            $ResultsPerPage = 5000
        }
    }

    $OAuthParameters = [OAuthParameters]::new(
        'Get',
        $null,
        $null
    )

    $Query = [hashtable]::new()
    if ($ScreenName) {
        $Query.Add('screen_name',$ScreenName)
    }
    if ($UserId) {
        $Query.Add('user_id',$UserId)
    }
    $Query.Add('count',$ResultsPerPage)

    if ($PSCmdlet.ParameterSetName -eq 'List') {
        $OAuthParameters.BaseUri = 'https://api.twitter.com/1.1/friends/list.json'

        if ($PSBoundParameters.ContainsKey('SkipStatus')) {
            $Query.Add('skip_status','true')
        }
        if ($PSBoundParameters.ContainsKey('ExcludeEntities')) {
            $Query.Add('include_entities','false')
        } else {
            $Query.Add('include_entities','true')
        }

    } else {
        $OAuthParameters.BaseUri = 'https://api.twitter.com/1.1/friends/ids.json'
    }

    $OAuthParameters.SetQuery($Query)

    if ($PSCmdlet.ParameterSetName -eq 'List') {
        $TwitterFriends = Invoke-TwitterCursorRequest -OAuthParameters $OAuthParameters
        @($TwitterFriends.users)
    } else {
        $TwitterFriends = Invoke-TwitterCursorRequest -OAuthParameters $OAuthParameters
        @($TwitterFriends.ids)
    }

}


#