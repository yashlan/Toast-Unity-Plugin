

@interface Toast_iOS : UIViewController

@property (nonatomic, strong) UILabel *toastLabel;
+ (instancetype)sharedInstance;
- (void)showToast:(NSString *)message withDuration:(float)duration;
- (void)cancelToast;

@end

@implementation Toast_iOS

@synthesize toastLabel;

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
    lab.numberOfLines = 0; // Allow multiline
    
    CGFloat bottomMargin = 100;

    CGSize textSize = [message sizeWithAttributes:@{NSFontAttributeName: lab.font}];
    CGFloat labelWidth = textSize.width + 20;
        
    CGFloat maxWidth = rootViewController.view.frame.size.width - 40;
    labelWidth = MIN(labelWidth, maxWidth);
    
    CGSize maxSize = CGSizeMake(rootViewController.view.frame.size.width - 40, CGFLOAT_MAX);
    lab.frame = CGRectMake(20, rootViewController.view.frame.size.height - bottomMargin, maxSize.width, 0);

    CGSize labelSize = [lab sizeThatFits:maxSize];
    lab.frame = CGRectMake((rootViewController.view.frame.size.width - labelWidth) / 2,
                           rootViewController.view.frame.size.height - labelSize.height - bottomMargin,
                           labelWidth,
                           labelSize.height + 20);
    
    [rootViewController.view addSubview:lab];
    
    self.toastLabel = lab;
    
    [UIView animateWithDuration:duration delay:duration options:UIViewAnimationOptionCurveEaseOut animations:^{
        lab.alpha = 0.0;
    } completion:^(BOOL isCompleted) {
        [lab removeFromSuperview];
    }];
}

- (void) cancelToast {
    if (self.toastLabel) {
        [self.toastLabel.layer removeAllAnimations];
        [self.toastLabel removeFromSuperview];
        self.toastLabel = nil;
    }
}

@end

extern "C" {

    void showToast(const char * message, float duration)
    {
        NSString *messageString = [NSString stringWithUTF8String:message];
        [[Toast_iOS sharedInstance] showToast:messageString withDuration:duration];
    }

    void cancelToast()
    {
        [[Toast_iOS sharedInstance] cancelToast];
    }
}
