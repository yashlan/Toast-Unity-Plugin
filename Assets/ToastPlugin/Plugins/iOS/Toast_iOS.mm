

@interface Toast_iOS : UIViewController

+ (instancetype)sharedInstance;
- (void)showToast:(NSString *)message withDuration:(float)duration;

@end

@implementation Toast_iOS

+ (instancetype) sharedInstance {
    static Toast_iOS *sharedInstance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[Toast_iOS alloc] init];
    });
    return sharedInstance;
}

- (void)showToast:(NSString *)message withDuration:(float)duration {
    UIViewController *rootViewController = UnityGetGLViewController();
    UILabel *lab = [[UILabel alloc] init];
    lab.backgroundColor = [[UIColor blackColor] colorWithAlphaComponent:1.0];
    lab.textColor = [UIColor whiteColor];
    lab.font = [UIFont systemFontOfSize:17.0];
    lab.textAlignment = NSTextAlignmentCenter;
    lab.text = message;
    lab.alpha = 1.0;
    lab.layer.cornerRadius = 10;
    lab.clipsToBounds = YES;
    
    CGSize textSize = [message sizeWithAttributes:@{NSFontAttributeName: lab.font}];
    CGFloat labelWidth = textSize.width + 20;
    
    CGFloat maxWidth = rootViewController.view.frame.size.width - 40;
    labelWidth = MIN(labelWidth, maxWidth);
    
    lab.frame = CGRectMake((rootViewController.view.frame.size.width - labelWidth) / 2, rootViewController.view.frame.size.height - 100, labelWidth, 50);
    
    [rootViewController.view addSubview:lab];

    
    [UIView animateWithDuration:duration delay:0.1 options:UIViewAnimationOptionCurveEaseOut animations:^{
        lab.alpha = 0.0;
    } completion:^(BOOL isCompleted) {
        [lab removeFromSuperview];
    }];
}

@end

extern "C" {

    void showToast(const char * message, float duration)
    {
        NSString *messageString = [NSString stringWithUTF8String:message];
        [[Toast_iOS sharedInstance] showToast:messageString withDuration:duration];
    }
}
