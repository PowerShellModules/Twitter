function New-TwitterQuery {
    [CmdletBinding()]
    param(
        [hashtable]$ApiParameters
    )

    $TwitterQuery = [hashtable]::new()

    foreach ($Parameter in $ApiParameters.Keys) {
        switch ($Parameter) {
            'ScreenName' {
                # GET friendships/lookup allows for multiple input
                if ($ApiParameters[$Parameter] -is [array]) {
                    $TwitterQuery.Add('screen_name',($ApiParameters[$Parameter] -join ','))
                } else {
                    $TwitterQuery.Add('screen_name',$ApiParameters[$Parameter])
                }
            }
            'UserId' {
                # GET friendships/lookup allows for multiple input
                if ($ApiParameters[$Parameter] -is [array]) {
                    $TwitterQuery.Add('user_id',($ApiParameters[$Parameter] -join ','))
                } else {
                    $TwitterQuery.Add('user_id',$ApiParameters[$Parameter])
                }
            }
            'Count' {
               $TwitterQuery.Add('count',$ApiParameters[$Parameter])
            }
            'TweetId' {
                $TwitterQuery.Add('id',$ApiParameters[$Parameter])
            }
            'SinceId' {
                $TwitterQuery.Add('since_id',$ApiParameters[$Parameter])
            }
            'MaxId' {
                $TwitterQuery.Add('max_id',$ApiParameters[$Parameter])
            }
            'SkipStatus' {
                $TwitterQuery.Add('skip_status',$ApiParameters[$Parameter])
            }
            'ResultsPerPage' {
                $TwitterQuery.Add('count',$ApiParameters[$Parameter])
            }
            'SourceScreenName' {
                $TwitterQuery.Add('source_screen_name',$ApiParameters[$Parameter])
            }
            'SourceUserId' {
                $TwitterQuery.Add('source_id',$ApiParameters[$Parameter])
            }
            'TargeteScreenName' {
                $TwitterQuery.Add('target_screen_name',$ApiParameters[$Parameter])
            }
            'TargetUserId' {
                $TwitterQuery.Add('target_id',$ApiParameters[$Parameter])
            }
        }
    }

    if ($ApiParameters.ContainsKey('ExcludeEntities')) {
        $TwitterQuery.Add('include_entities','false')
    } else {
        $TwitterQuery.Add('include_entities','true')
    }

    '{0}Twitter Query:{0}{1}' -f [System.Environment]::NewLine,($TwitterQuery.GetEnumerator() | Out-String).Trim() | Write-Verbose
    $TwitterQuery
}
